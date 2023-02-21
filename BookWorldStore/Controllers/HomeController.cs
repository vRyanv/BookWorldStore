using BookWorldStore.Models;
using BookWorldStore.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
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

        public async Task<IActionResult> Index(int? id)
        {
            User user = Utils.UserUtils.Instance.GetUser(HttpContext);
            if(user != null)
            {
                ViewBag.loggedIn = true;
            }

            var cateList = await dbContext.categories.Where(c => c.status == 1).ToListAsync();
            ICollection<Book> bookList = new List<Book>();
            if (id != null)
            {
                bookList = await dbContext.books.Where(b => b.status == 1 && b.cate_id == id).Include(c => c.category).Include(s => s.supplier).ToListAsync();
            }
            else
            {
                bookList = await dbContext.books.Where(b => b.status == 1).Include(c => c.category).Include(s => s.supplier).ToListAsync();
            }

            ViewData["cateList"] = cateList;
            return View(bookList);
        }

        public async Task<IActionResult> SearchBook([FromQuery(Name = "title")]string title)
        {
            User user = Utils.UserUtils.Instance.GetUser(HttpContext);
            if (user != null)
            {
                ViewBag.loggedIn = true;
            }

            var cateList = await dbContext.categories.Where(c => c.status == 1).ToListAsync();
            ICollection<Book> bookList = bookList = await dbContext.books.Where(b => b.status == 1 && b.title.Contains(title)).Include(c => c.category).Include(s => s.supplier).ToListAsync();
            
            ViewData["cateList"] = cateList;
            return View("Index", bookList);
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
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                User exisEmail = await dbContext.users.Where(e => e.email == user.email).FirstAsync();
                if(exisEmail == null)
                {
                    user.status = 0;
                    user.role = "client";
                    user.token_reset_pass = "";
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    return RedirectToAction("Login");
                }
                ViewBag.error = "Email is exist";
                return View("~/Views/Service/Register.cshtml");
            }
            ViewBag.error = "Some field is wrong";
            return View("~/Views/Service/Register.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> SendResetPassRequest([FromQuery(Name = "email")] string email)
        {
            User user = await dbContext.users.Where(u => u.email == email).FirstOrDefaultAsync();
        
            if (user != null)
            {
                user.status = 1;
                dbContext.SaveChanges();

                ViewBag.email = email;
                ViewBag.notification = "<p>Your request has been sent <br/> We will respond to your request via email: ";
                return View("ResullSuccess");
            }
            ViewBag.email = email;
            return View("ResultFail");
        }

        public IActionResult NotFoundPage()
        {
            return View("_Page404");
        }
    }
}