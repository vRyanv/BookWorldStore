using BookWorldStore.Models;
using BookWorldStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookWorldStore.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDBContext dbContext;
        public DashboardController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize(Roles = "owner, admin")]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
