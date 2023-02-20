using BookWorldStore.Models;
using BookWorldStore.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace BookWorldStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext dbContext;
        public HomeController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            User user = Utils.UserUtils.Instance.GetUser(HttpContext);
            if(user != null)
            {
                ViewBag.loggedIn = true;
            }

            ICollection<Book> bookList = await dbContext.books.Where(b => b.status == 1).Include(c => c.category).Include(s => s.supplier).ToListAsync();

            return View(bookList);
        }

        public async Task<IActionResult> Detail(int id)
        {
            User user = Utils.UserUtils.Instance.GetUser(HttpContext);
            if (user != null)
            {
                ViewBag.loggedIn = true;
            }

            ICollection<Book> bookList = await dbContext.books.Where(b => b.status == 1 && b.book_id == id)
                                    .Include(c => c.category).Include(s => s.supplier)
                                    .ToListAsync();
            return View(bookList);
        }

		public IActionResult Login()
		{
			return View("~/Views/Service/Login.cshtml");
		}

        public IActionResult Register()
		{
			return View("~/Views/Service/Register.cshtml");
		}

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.status = 0;
                user.role = "client";
                user.token_reset_pass = "";
                dbContext.Add(user);
                dbContext.SaveChanges();
                return RedirectToAction("Login");
            }
            return View("~/Views/Service/Register.cshtml");
        }

        [HttpGet]
        public IActionResult SendResetPassRequest([FromQuery(Name = "email")]string email)
        {
            ViewBag.email = email;
            return View("ResullSuccess");
        }

        public IActionResult NotFoundPage()
        {
            return View("_Page404");
        }
    }
}