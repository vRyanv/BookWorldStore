using BookWorldStore.Models;
using BookWorldStore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookWorldStore.Controllers
{
    public class ProfileController : Controller
    {
        private readonly AppDBContext dbContext;
        public ProfileController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {

            var user = UserUtils.Instance.GetUser(this.HttpContext);
            var result = await dbContext.users.Where(u => u.email == user.email).FirstAsync();
            return View();
        }


    }
}
