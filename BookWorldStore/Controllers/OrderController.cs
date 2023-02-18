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
        
        public async Task<IActionResult> OrderDetail(int id)
        {
            var result= await dbContext.orderDetails.Include("order").Include("book").Where(od=>od.order.order_id==id).ToListAsync();
            return View("~/Views/Admin/Order/OrderDetail.cshtml", result);
        }
    }
}
