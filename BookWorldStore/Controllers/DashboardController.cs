using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookWorldStore.Controllers
{
    public class DashboardController : Controller
    {
        [Authorize(Roles = "owner, admin")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
