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
        public IActionResult ChangesImformation(ProfileViewModel profile) {

            var user = UserUtils.Instance.GetUser(this.HttpContext);
            User infor= dbContext.users.Where(u => user.email == u.email).First();
            infor.phone = profile.phone;
            infor.gender = profile.gender;
            infor.address = profile.address;
            dbContext.Update(infor);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ChangesPassword()
        {
            return View();
        }
    }
}
