
using APIService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace APIService.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class StatisticalController : Controller
    {
        private readonly APIContext _APIContext;
        public StatisticalController(APIContext apiContext)
        {
            _APIContext = apiContext;
        }

        public class DateStati
        {
            public string start_date { get; set; }
            public string end_date { get; set; }
        }

        [HttpPost]
        [Consumes("application/json")]
        public IActionResult GetStatistical(DateStati dateStati)
        {
            string format = "yyyy-MM-dd";
            DateTime start = DateTime.ParseExact(dateStati.start_date, format, CultureInfo.InvariantCulture);
            DateTime end = DateTime.ParseExact(dateStati.end_date, format, CultureInfo.InvariantCulture);
            List<StatisticalViewModel> statistical_= new List<StatisticalViewModel>();
                var result = (from od in _APIContext.orderDetails
                    join b in _APIContext.books on od.book.book_id equals b.book_id
                    join o in _APIContext.orders on od.order_id equals o.order_id
                    group new { od, b,o } by new { b.title, o.order_date, o.delivery_date,b.price,b.book_id,o.status } into g
                    select new StatisticalViewModel
                    {
                        Id=g.Key.book_id,
                        title = g.Key.title,
                        start=g.Key.order_date,
                        end=g.Key.delivery_date,
                        status=g.Key.status,
                        totalPrice = g.Sum(x => x.od.quantity)*g.Key.price,
                    }).Where(od => start >= od.start && end<=od.end && od.status==1).ToList().OrderByDescending(x=>x.totalPrice);
            foreach(StatisticalViewModel item in result)
            {
                statistical_.Add(item);
                
            }
            return Ok(statistical_);
        }
    }
}
