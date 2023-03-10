using APIService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace APIService.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class TokenController : ControllerBase
    {
        private readonly APIContext _APIContext;
        private readonly IConfiguration configuration;
        public TokenController(APIContext _APIContext, IConfiguration configuration)
        {
            this._APIContext = _APIContext;
            this.configuration = configuration;
        }

        [HttpPost]
        [Consumes("application/json")]
        public async Task<IActionResult> Login(User _user)
        {
            _user = await AuthenUser(_user);
            if (_user.role != null)
            {
                string tokenStr = GenerateJSONWebToken(_user);
                var data  = new { status = 200, role = _user.role,token = tokenStr };
                return Content(JsonSerializer.Serialize(data));
            }
            else
            {
                var data = new { status = 400, message = "authentication fail" };
                return Content(JsonSerializer.Serialize(data));
            }
        }

        private async Task<User> AuthenUser(User _user)
        {
           var result = await _APIContext.users.Where(u => u.email.Equals(_user.email)&& u.password.Equals(_user.password)).ToListAsync();

            if(result.Count > 0)
            {
                _user.user_id = result[0].user_id;
                _user.role = result[0].role;
            }
            return _user;
        }

        private string GenerateJSONWebToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                                new Claim("id", user.user_id.ToString()),
                                new Claim("email", user.email),
                                new Claim("role", user.role),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                     };
            var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: signIn
                );

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok("API is running");
        }

        [HttpGet]
        [Authorize(Roles = "client")]
        public IActionResult Get()
        {
            return Ok("asd");
        }
    }
}
