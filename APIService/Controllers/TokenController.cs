using APIService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly APIContext _APIContext;
        private readonly IConfiguration configuration;
        public TokenController(APIContext _APIContext, IConfiguration configuration)
        {
            this._APIContext = _APIContext;
            this.configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel _user)
        {
            _user = await AuthenUser(_user);
            
            if(_user.role != "")
            {
                string tokenStr = GenerateJSONWebToken(_user);
                Response.Cookies.Append("__UserToken", tokenStr);
                return Ok(Json("login success"));
            }
            return Unauthorized();
        }

        public async Task<UserViewModel> AuthenUser(UserViewModel _user)
        {
           var result = await _APIContext.users.Where(u => u.email.Equals(_user.email)&& u.password.Equals(_user.password)).ToListAsync();

            if(result.Count > 0)
            {
                _user.role = result[0].role;
            }
            return _user;
        }

        public string GenerateJSONWebToken(UserViewModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                                new Claim(ClaimTypes.Email, user.email),
                                new Claim(ClaimTypes.Role, user.role),
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
            return Json("API is running");
        }
    }
}
