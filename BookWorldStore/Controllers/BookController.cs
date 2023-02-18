using BookWorldStore.Helper;
using BookWorldStore.Models;
using BookWorldStore.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWorldStore.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDBContext dbContext;
        public BookController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var book = dbContext.books.Include("category").Include("supplier").ToList();
            return View("~/Views/Admin/Book/Index.cshtml",book);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var cateList = await dbContext.categories.ToListAsync();
            var supList = await dbContext.suppliers.ToListAsync();
            ViewData["cateList"] = cateList;
            ViewData["supList"] = supList;
            return View("~/Views/Admin/Book/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Book book, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                string folder = "img/book";
                string fileName = await FileHelper.Instance.SaveFileAsync(image, folder);
                book.image = fileName;
                book.status = 1;

                dbContext.Add(book);
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        [HttpGet]
        async public Task<IActionResult> Edit(int id)
        {
            return View("~/Views/Admin/Book/Edit.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            string folder = "img/book";
            if (FileHelper.Instance.DeleteFileAsync(folder, fileName))
            {
              return Ok("success delete");
            }
            return BadRequest();
           
        }

    }
}
