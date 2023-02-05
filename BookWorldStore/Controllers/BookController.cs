using BookWorldStore.Models;
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
    }
}
