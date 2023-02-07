using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWorldStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDBContext dbContext;
        public ProfileController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
