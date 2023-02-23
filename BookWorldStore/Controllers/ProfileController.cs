using BookWorldStore.Models;
using BookWorldStore.Repository;
using BookWorldStore.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

        [Authorize(Roles = "client, owner, admin")]
        public async Task<IActionResult> Index()
        {

            var user = UserUtils.Instance.GetUser(this.HttpContext);
            ProfileViewModel result = await dbContext.users.Select(p=>new ProfileViewModel
            {
                email=p.email,
                address=p.address,
                phone=p.phone,
                gender=p.gender,
            }).Where(u=> user.email==u.email).FirstOrDefaultAsync();

            ViewBag.loggedIn = true;
            return View(result);
        }

        [Authorize(Roles = "client, owner, admin")]
        public IActionResult ChangesImformation(ProfileViewModel profile) {

            var user = UserUtils.Instance.GetUser(this.HttpContext);
            User infor= dbContext.users.Where(u => user.email == u.email).FirstOrDefault();
            infor.phone = profile.phone;
            infor.gender = profile.gender;
            infor.address = profile.address;
            dbContext.Update(infor);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "client, owner, admin")]
        public async Task<IActionResult> ChangesPassword([FromForm] string currentPass, [FromForm] string newPass, [FromForm] string confirmPass)
        {
            string notification = "";
            string email = UserUtils.Instance.GetUser(HttpContext).email;
            User user = await dbContext.users.Where(u => u.email == email && u.password == currentPass).FirstOrDefaultAsync();
            if(user != null)
            {
                if(newPass == confirmPass)
                {
                    user.password = newPass;
                    await dbContext.SaveChangesAsync();
                    notification = "Change password success";
                }
                else
                {
                    notification = "Confimr password not match";
                }
            }
            else
            {
                notification = "Current password is wrong";
            }

            TempData["notification"] = notification;
            return RedirectToAction("Index");
        }
    }
}
