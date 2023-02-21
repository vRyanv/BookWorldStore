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

        [Authorize(Roles = "client, owner, admin")]
        public IActionResult Index()
        {
            User user = UserUtils.Instance.GetUser(HttpContext);
            
            return View();
        }

        [Authorize(Roles = "client, owner, admin")]
        public IActionResult AddToCart(int id)
        {
            int userId = UserUtils.Instance.GetUser(HttpContext).user_id;
            var result = dbContext.orders.Where(o => o.user_id == userId && o.status == 0);
            Order order = new Order();
            
            order.user_id = id;
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "client, owner, admin")]
        public IActionResult OldOrder()
        {
            return View();
        }
    }
}
