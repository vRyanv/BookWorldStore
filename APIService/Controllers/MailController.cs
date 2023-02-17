using APIService.Models;
using BookWorldStore.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIService.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class MailController : ControllerBase
    {
        private readonly APIContext _APIContext;
        public MailController(APIContext _APIContext)
        {
            this._APIContext = _APIContext;
        }

        [HttpPost]
        public async Task<IActionResult> RequestResetPass([FromBody]string email)
        {
            string linkAcceptResetPass = "https://localhost:44378/api/Mail/HandleResetPass";
            await MailHelper.Instance.SendEmail(email, "request reset pasword", linkAcceptResetPass);
            var data = new { status = 200, message = $"Request reset pass for email: {email} is success" };
            return Content(JsonSerializer.Serialize(data));
        }
    }
}
