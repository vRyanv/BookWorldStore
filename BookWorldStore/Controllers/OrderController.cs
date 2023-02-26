using BookWorldStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookWorldStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDBContext dbContext;
        public OrderController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Authorize(Roles = "client, owner, admin")]
        public IActionResult Index()
        {
            var result=dbContext.orders.Where(o => o.status == 1).Include("user").ToList();
            return View("~/Views/Admin/Order/Index.cshtml",result);
        }

        [Authorize(Roles = "client, owner, admin")]
        public async Task<IActionResult> OrderDetail(int id)
        {
            var result= await dbContext.orderDetails.Include("order").Include("book").Where(od=>od.order.order_id==id).ToListAsync();
            return View("~/Views/Admin/Order/OrderDetail.cshtml", result);
        }
    }
}
