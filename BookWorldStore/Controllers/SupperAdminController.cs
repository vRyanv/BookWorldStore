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
        public IActionResult CustomerRequest()
        {
            return View("~/Views/Admin/SupperAdmin/CustomerRequest.cshtml");
        }

        [HttpGet]
        public IActionResult OwnerRequest()
        {
            return View("~/Views/Admin/SupperAdmin/OwnerRequest.cshtml");
        }

        [HttpGet]
        public IActionResult AgreeResetPass([FromQuery] string receiverMail)
        {
            User user = dbContext.users.Where(u => u.email == receiverMail).First();
            if(user != null)
            {
                string subject = "Response to password reset request";
                string message = "<a href='http://localhost:7048/SupperAdmin/ResetPass' >Click<>";
                MailHelper.Instance.SendEmail(receiverMail, subject, message);
                user.status = 0;
                dbContext.SaveChanges();
            }
            return View("~/Views/Admin/SupperAdmin/CustomerRequest.cshtml");
        }

        [HttpGet]
        public IActionResult GetNewPass()
        {
            return View()
        }


        [HttpPost]
        public async Task<IActionResult> RequestResetPass([FromBody] string receiverMail)
        {
            string tokenResetPass = Guid.NewGuid().ToString();
            User user = dbContext.users.Where(u => u.email == receiverMail).First();
            user.token_reset_pass = tokenResetPass;
            dbContext.SaveChanges();

            string linkAcceptResetPass = $"<a href='https://localhost:44378/api/Mail/HandleResetPass?email={receiverMail}&token={tokenResetPass}' style='color:red'></a>";
            string subject = "request reset pasword";
            await MailHelper.Instance.SendEmail(receiverMail, subject, linkAcceptResetPass);
            return RedirectToAction("CategoryRequest");
        }
    }
}
