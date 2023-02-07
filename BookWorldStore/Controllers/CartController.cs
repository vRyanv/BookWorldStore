using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDBContext dbContext;
        public CartController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
