using BookWorldStore.Helper;
using BookWorldStore.Models;
using BookWorldStore.Models.Repository;
using Microsoft.AspNetCore.Mvc;

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
            return View("~/Views/Admin/Book/Index.cshtml");
        }

        [HttpGet]
        async public Task<IActionResult> Create()
        {
            return View("~/Views/Admin/Book/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            return View();
        }

        [HttpGet]
        async public Task<IActionResult> Edit()
        {
            return View("~/Views/Admin/Book/Edit.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> AddBookTest()
        {
            return View("~/Views/Admin/Book/TestAddBook.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> AddBookTest(BookTest book, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                string directory = "img/book";
                string fileName = await FileHelper.Instance.SaveFileAsync(imageFile, directory);

                book.imageFile = $"fileName: {fileName}";
                return Ok(book);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFile(string path)
        {

            if (FileHelper.Instance.DeleteFileAsync(path))
            {
              return Ok("success delete");
            }
            return BadRequest();
           
        }




    }
}
