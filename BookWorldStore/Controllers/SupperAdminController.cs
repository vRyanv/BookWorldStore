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
        public IActionResult CategoryRequest()
        {
            return View("~/Views/Admin/SupperAdmin/CategoryRequest.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> RequestResetPass([FromBody] string receiverMail)
        {
            string tokenResetPass = Guid.NewGuid().ToString();
            var user = dbContext.users.Where(u => u.email == receiverMail);

            string linkAcceptResetPass = $"<a href='https://localhost:44378/api/Mail/HandleResetPass?email={receiverMail}&token={tokenResetPass}' style='color:red'></a>";
            string subject = "request reset pasword";
            await MailHelper.Instance.SendEmail(receiverMail, subject, linkAcceptResetPass);
            return RedirectToAction("CategoryRequest");
        }
    }
}
