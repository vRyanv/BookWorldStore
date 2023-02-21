using BookWorldStore.Helper;
using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.Json;

namespace BookWorldStore.Controllers
{
    public class SupperAdminController : Controller
    {
        private readonly AppDBContext dbContext;
        public SupperAdminController(AppDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> CategoryRequest()
        {
            List<Category> cateList = await dbContext.categories.Where(c => c.status == 2).ToListAsync();
            return View("~/Views/Admin/SupperAdmin/CategoryRequest.cshtml", cateList);
        }

        [HttpGet]
        public async Task<IActionResult> AgreeCate(int id)
        {
            Category cate = await dbContext.categories.Where(c => c.cate_id == id).FirstOrDefaultAsync();
            if(cate != null)
            {
                cate.status = 1;
                dbContext.SaveChanges();
                return RedirectToAction("CategoryRequest");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> RefuseCate(int id)
        {
            Category cate = await dbContext.categories.Where(c => c.cate_id == id).FirstOrDefaultAsync();
            if (cate != null)
            {
                dbContext.Remove(cate);
                dbContext.SaveChanges();
                return RedirectToAction("CategoryRequest");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> CustomerRequest()
        {
            List<User> user = await dbContext.users.Where(u => u.status == 1 && u.role == "client").ToListAsync();

            ViewBag.title = "Customer Request";
            return View("~/Views/Admin/SupperAdmin/CustomerRequest.cshtml", user);
        }

        [HttpGet]
        public async Task<IActionResult> OwnerRequest()
        {
            List<User> user = await dbContext.users.Where(u => u.status == 1 && u.role == "owner").ToListAsync();
            ViewBag.title = "Owner Request";
            return View("~/Views/Admin/SupperAdmin/OwnerRequest.cshtml", user);
        }

        [HttpGet]
        public async Task<IActionResult> AgreeResetPass([FromQuery(Name = "email")] string email)
        {
            User user = await dbContext.users.Where(u => u.email == email).FirstOrDefaultAsync();
            if(user != null)
            {
                string tokenResetPass = Guid.NewGuid().ToString();
                user.token_reset_pass = tokenResetPass;
                user.status = 0;
                dbContext.SaveChanges();
                string subject = "Response to password reset request";
                string message = $"<h3>Your reset request has been accepted</h3><br/><a href='http://book.fpt.com:8080/SupperAdmin/GetNewPass?email={email}&&token={tokenResetPass}'>Click here to get new password</a>";
                await MailHelper.Instance.SendEmail(email, subject, message);


                if (user.role == "client")
                {
                    return RedirectToAction("CustomerRequest");
                }
                else
                {
                    return RedirectToAction("OwnerRequest");
                }
            }

            return NotFound();
            
        }

        [HttpGet]
        public async Task<IActionResult> RefuseResetPass([FromQuery(Name = "email")] string email)
        {
            User user = await dbContext.users.Where(u => u.email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                user.status = 0;
                dbContext.SaveChanges();
                string subject = "Response to password reset request";
                string message = "We refused your request to reset your password because you broke some laws";
                await MailHelper.Instance.SendEmail(email, subject, message);


                if (user.role == "client")
                {
                    return RedirectToAction("CustomerRequest");
                }
                else
                {
                    return RedirectToAction("OwnerRequest");
                }
            }

            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult> GetNewPass([FromQuery(Name = "email")] string email, [FromQuery(Name = "token")] string token)
        {
            User user = dbContext.users.Where(u => u.email == email && u.token_reset_pass == token).FirstOrDefault();
            if(user != null)
            {
                string newPass = Guid.NewGuid().ToString().Substring(0,8);
                user.password = newPass;
                user.token_reset_pass = "";
                dbContext.SaveChanges();
                string subject = "New password";
                await MailHelper.Instance.SendEmail(email, subject, $"This is new pass: {newPass}");

                ViewBag.email = email;
                ViewBag.notification = "<p>Check in your mail<br/>The password was reseted for";
                return View("~/Views/Home/ResullSuccess.cshtml");
            }

            return NotFound();
            
        }

    }
}
