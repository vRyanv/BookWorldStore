using BookWorldStore.Models;
using BookWorldStore.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookWorldStore.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDBContext dbContext;
        public CartController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            User user = UserUtils.Instance.GetUser(HttpContext);
            ViewBag.user_mail = user.email;
            return View();
        }

        [Authorize]
        public IActionResult OldOrder()
        {
            return View();
        }
    }
}
