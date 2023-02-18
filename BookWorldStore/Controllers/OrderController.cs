using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookWorldStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDBContext dbContext;
        public OrderController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var result=dbContext.orders.Include("user").ToList();
            return View("~/Views/Admin/Order/Index.cshtml",result);
        }
    }
}
