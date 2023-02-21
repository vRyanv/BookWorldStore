using BookWorldStore.Models;
using BookWorldStore.Repository;
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
            if (order == null)
            {
                Order newOrder = new Order();
                newOrder.user_id = userId;
                newOrder.status = 0;
                dbContext.Add(newOrder);
                await dbContext.SaveChangesAsync();
                ViewData["list"] = await dbContext.orderDetails.Include("book").Join(dbContext.orders, od => od.order_id, o => o.order_id, (od, o) => new OldOrderViewModel
                {
                    id = od.order_detail_id,
                    image = od.book.image,
                    title = od.book.title,
                    price = od.book.price,
                    quantity = od.quantity,
                    total = od.book.price * od.quantity,
                    orderid=o.order_id,
                    date = o.delivery_date,
                    status = o.status,
                    user_id = o.user_id,

                }).Where(od => od.status == 0 && od.user_id == userId).ToListAsync();
            }
            else
            {
                ViewData["list"] = await dbContext.orderDetails.Include("book").Join(dbContext.orders, od => od.order_id, o => o.order_id, (od, o) => new OldOrderViewModel
                {
                    id = od.order_detail_id,
                    image = od.book.image,
                    title = od.book.title,
                    price = od.book.price,
                    quantity = od.quantity,
                    total = od.book.price * od.quantity,
                    orderid=o.order_id,
                    date = o.delivery_date,
                    status = o.status,
                    user_id = o.user_id,

                }).Where(od => od.status == 0 && od.user_id == userId).ToListAsync();
            }

            ViewBag.loggedIn = true;
            var result = await dbContext.orderDetails.Include("book").Join(dbContext.orders, od => od.order_id, o => o.order_id, (od, o) => new OldOrderViewModel
            {
                id = od.order_detail_id,
                image = od.book.image,
                title = od.book.title,
                price = od.book.price,
                quantity = od.quantity,
                total = od.book.price * od.quantity,
                date = o.delivery_date,
                status = o.status,
                user_id = o.user_id,

            }).Where(od => od.status == 1 && od.user_id == userId).ToListAsync();
            ViewData["history"] = result;
            return View();
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
        public async Task<IActionResult> Remove(int id)
        {
            OrderDetail orderDetail=dbContext.orderDetails.Find(id);
            dbContext.Remove(orderDetail);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Increase(int id)
        {
            OrderDetail orderDetail = dbContext.orderDetails.Find(id);
            orderDetail.quantity++;
            dbContext.Update(orderDetail);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Decrease(int id)
        {
            OrderDetail orderDetail = dbContext.orderDetails.Find(id);
            if(orderDetail.quantity > 0)
            {
                orderDetail.quantity--;
                dbContext.Update(orderDetail);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
            
            
        }
    }
}
