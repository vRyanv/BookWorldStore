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
                string[] result = await FileHelper.Instance.SaveFileAsync(imageFile, directory);

                book.imageFile = $"fileName: {result[0]} - filePath: {result[1]}";
                return Ok(book);
            }

            return NotFound();
        }




    }
}
