using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/Order/Index.cshtml");
        }
    }
}
