using BookWorldStore.Models;
using BookWorldStore.Repository;
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
            ProfileViewModel result = await dbContext.users.Select(p=>new ProfileViewModel
            {
                email=p.email,
                address=p.address,
                phone=p.phone,
                gender=p.gender,
            }).Where(u=> user.email==u.email).FirstAsync();
            return View(result);
        }


    }
}
