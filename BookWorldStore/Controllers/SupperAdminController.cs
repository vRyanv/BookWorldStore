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
        public IActionResult CategoryRequest()
        {
            return View("~/Views/Admin/SupperAdmin/CategoryRequest.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> CustomerRequest()
        {
            List<User> user = await dbContext.users.Where(u => u.status == 1 && u.role == "client").ToListAsync();
            return View("~/Views/Admin/SupperAdmin/CustomerRequest.cshtml", user);
        }

        [HttpGet]
        public IActionResult OwnerRequest()
        {
            return View("~/Views/Admin/SupperAdmin/OwnerRequest.cshtml");
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
                string message = $"<a href='http://localhost:7048/SupperAdmin/GetNewPass?email={email}&&token={tokenResetPass}'>Click here to get new password</a>";
                await MailHelper.Instance.SendEmail(email, subject, message);


                if (user.role == "client")
                {
                    return View("~/Views/Admin/SupperAdmin/CustomerRequest.cshtml");
                }
                else
                {
                    return View("~/Views/Admin/SupperAdmin/OwnerRequest.cshtml");
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
                    return View("~/Views/Admin/SupperAdmin/CustomerRequest.cshtml");
                }
                else
                {
                    return View("~/Views/Admin/SupperAdmin/OwnerRequest.cshtml");
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
                string newPass = "123";
                user.password = newPass;
                dbContext.SaveChanges();
                string subject = "New password";
                await MailHelper.Instance.SendEmail(email, subject, $"new pass: {newPass}");
                return Json("view in mail");
            }

            return NotFound();
            
        }

    }
}
