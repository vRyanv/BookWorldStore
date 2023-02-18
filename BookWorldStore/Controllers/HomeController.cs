using BookWorldStore.Models;
using BookWorldStore.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index()
        {
            User user = Utils.UserUtils.Instance.GetUser(HttpContext);
            if(user != null)
            {
                ViewBag.loggedIn = true;
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public IActionResult Detail(int id)
        {
            User user = Utils.UserUtils.Instance.GetUser(HttpContext);
            if (user != null)
            {
                ViewBag.loggedIn = true;
            }
            return View();
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
                user.status = "";
                user.role = "client";
                user.token_reset_pass = "";
                dbContext.Add(user);
                dbContext.SaveChanges();
                return RedirectToAction("Login");
            }
            return View("~/Views/Service/Register.cshtml");
        }

        public IActionResult NotFoundPage()
        {
            return View("_Page404");
        }


        public IActionResult test()
        {
            //List<BookViewModel> books = new BookViewModel(dbContext).Showall();
            //foreach (var b in books) {
            //    Console.WriteLine($"{b.title} {b.cate_name} {b.sup_name}");
            //}
            List<StatisticalHotBookViewModel>hotbooks=new StatisticalHotBookViewModel(dbContext).showHistory();
            foreach (var hotbook in hotbooks)
            {
                Console.WriteLine($"{hotbook.title}-{hotbook.quantity}");
            }
           return Redirect("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}