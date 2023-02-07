using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
