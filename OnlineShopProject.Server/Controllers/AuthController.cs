using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineShopProject.Server.Data;
using OnlineShopProject.Server.Interfaces;
using OnlineShopProject.Server.Models;
using System.Security.Claims;
using ZstdSharp.Unsafe;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace OnlineShopProject.Server.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {

        private readonly DataContext _DataContext;
        private readonly IUserRepository _UserRepository;

        public AuthController(DataContext datacontext, IUserRepository userRepository)
        {
            _DataContext = datacontext;
            _UserRepository = userRepository;
        }

        [HttpPost("login")]

        public IActionResult Login([FromBody] LoginModel User)
        {
            User UserFinded = _UserRepository.GetUserByUsername(User.UserName);

            if(UserFinded is not null && _UserRepository.CheckPassword(UserFinded, User.Password))
            {

                var claims = new List<Claim>
                {

                    new Claim(ClaimTypes.Name, User.UserName),

                    new Claim(ClaimTypes.Surname, UserFinded.LastName),

                    new Claim(ClaimTypes.Email, UserFinded.Email)
                    
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SigningSecuritykey123456789_./5657"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:7013",
                    audience: "https://localhost:7013",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            
      

            return Unauthorized();
        }

        [Authorize(Policy = "RequireLoggedIn")]
        [HttpGet("secure-data")]
        public IActionResult GetSecureData()
        {
            return Ok("Esta es información segura");
        }
    }
}
