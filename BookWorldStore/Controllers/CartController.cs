using BookWorldStore.Models;
using BookWorldStore.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            int userId = UserUtils.Instance.GetUser(HttpContext).user_id;
            Order order = await dbContext.orders.Where(o => o.user_id == userId && o.status == 0).FirstOrDefaultAsync();
            List<OrderDetail> orderList = null;
            if (order == null)
            {
                Order newOrder = new Order();
                newOrder.user_id = userId;
                newOrder.status = 0;
                dbContext.Add(newOrder);
                await dbContext.SaveChangesAsync();
                orderList = await dbContext.orderDetails.Where(o => o.order_id == newOrder.order_id).ToListAsync();
            }
            else
            {
                orderList = await dbContext.orderDetails.Where(o => o.order_id == order.order_id).ToListAsync();
            }

            ViewBag.loggedIn = true;
            return View(orderList);
        }
        
        [HttpGet]
        [Authorize(Roles = "client, owner, admin")]
        public async Task<IActionResult> AddToCart(int id)
        {
            int userId = UserUtils.Instance.GetUser(HttpContext).user_id;
            Order order = await dbContext.orders.Where(o => o.user_id == userId && o.status == 0).FirstOrDefaultAsync();
           
            if (order != null)
            {
                OrderDetail orderDetail = await dbContext.orderDetails
                                .Where(od => od.order_id == order.order_id && od.book_id == id)
                                .FirstOrDefaultAsync();
                if(orderDetail != null)
                {
                    orderDetail.quantity += 1;
                }
                else
                {
                    OrderDetail newOrderDetail = new OrderDetail();
                    newOrderDetail.order_id = order.order_id;
                    newOrderDetail.book_id = id;
                    newOrderDetail.quantity = 1;
                    dbContext.Add(newOrderDetail);
                }
                dbContext.SaveChangesAsync();

            }
            else
            {
                order = new Order();
                order.user_id = userId;
                order.status = 0;
                dbContext.Add(order);
                await dbContext.SaveChangesAsync();

                OrderDetail newOrderDetail = new OrderDetail();
                newOrderDetail.order_id = order.order_id;
                newOrderDetail.book_id = id;
                newOrderDetail.quantity = 1;
                dbContext.Add(newOrderDetail);
                await dbContext.SaveChangesAsync();

            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "client, owner, admin")]
        public IActionResult OldOrder()
        {
            return View();
        }
    }
}
