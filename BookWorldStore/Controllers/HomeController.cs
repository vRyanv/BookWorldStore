using BookWorldStore.Models;
using BookWorldStore.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookWorldStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private readonly AppDBContext dbContext;
        public HomeController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult test()
        {
            //List<BookViewModel> books = new BookViewModel(dbContext).Showall();
            //foreach (var b in books) {
            //    Console.WriteLine($"{b.title} {b.cate_name} {b.sup_name}");
            //}
            List<StatisticalHotBookViewModel>hotbooks=new StatisticalHotBookViewModel(dbContext).showHistory();
            foreach (var hotbook in hotbooks)
            {
                Console.WriteLine($"{hotbook.title}-{hotbook.quantity}");
            }
           return Redirect("index");
           
        }
    }
}