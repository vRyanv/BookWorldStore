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
        public async Task<IActionResult> Create(Book book, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                string folder = "img/book";
                //string fileName = await FileHelper.Instance.SaveFileAsync(imageFile, folder);

                //book.image = $"fileName: {fileName}";
                return RedirectToAction("Index");
            }

            return View("~/Views/Admin/Book/Create.cshtml");
        }

        [HttpGet]
        async public Task<IActionResult> Edit()
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
