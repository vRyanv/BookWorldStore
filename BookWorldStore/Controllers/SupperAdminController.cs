using BookWorldStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            return View("~/Views/Admin/SupperAdmin/Index.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> RequestResetPass([FromBody] string email)
        {
            string tokenResetPass = Guid.NewGuid().ToString();
            var user = dbContext.users.Where(u => u.email == email);

            string linkAcceptResetPass = $"https://localhost:44378/api/Mail/HandleResetPass?email={email}&token={tokenResetPass}";
            //await MailHelper.Instance.SendEmail(email, "request reset pasword", linkAcceptResetPass);
            //var data = new { status = 200, message = $"Request reset pass for email: {email} is success" };
            return Content(JsonSerializer.Serialize(user));
        }
    }
}
