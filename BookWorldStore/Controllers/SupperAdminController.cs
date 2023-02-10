using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class SupperAdminController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Admin/SupperAdmin/Index.cshtml");
        }
    }
}
