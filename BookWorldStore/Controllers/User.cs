using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class User : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
