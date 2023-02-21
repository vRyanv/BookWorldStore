
using APIService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIService.Controllers
{
    public class statisticalController : Controller
    {
        private readonly APIContext _APIContext;
        public statisticalController(APIContext apiContext)
        {
            _APIContext = apiContext;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        public List<StatisticalViewModel> Statistical(DateTime start_date, DateTime end_time)
        {
            List<StatisticalViewModel> statistical_= new List<StatisticalViewModel>();
                var result =(from od in _APIContext.ordersDetail
                    join b in _APIContext.books on od.book.book_id equals b.book_id
                    join o in _APIContext.orders on od.order_id equals o.order_id
                    group new { od, b,o } by new { b.title, o.order_date, o.delivery_date } into g
                    select new StatisticalViewModel
                    {
                        title = g.Key.title,
                        start=g.Key.order_date,
                        end=g.Key.delivery_date,
                        quantity = g.Sum(x => x.od.quantity)
                    }).Where(od => start_date == od.start && end_time == od.end).ToList().OrderByDescending(x => x.quantity).Take(5);
            foreach(StatisticalViewModel item in result)
            {
                statistical_.Add(item);
            }
            return statistical_;
        }
    }
}
