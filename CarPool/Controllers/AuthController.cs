using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Configuration;

namespace CarPool.Controllers
{
    [ApiController]
    [Route("Api/Auth")]
    public class AuthController : ControllerBase
    {
        IUserService userService;
        private readonly IConfiguration configuration;
        public AuthController(IUserService user, IConfiguration config)
        {
            this.userService= user;
            this.configuration= config;
        }
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(User signup)
        {
            if (userService.IsValidSignup(signup)) {
                return Ok(signup);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(User user)
        {
            
            if (userService.IsValidLogin(user.emailId!,user.password!))
            {
                var token = GenerateJwtToken(user);
                return Ok(token);
                /*return Ok();*/
            }
            return NotFound();
        }
        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("asdv234234^&%&^%&^hjsdfb2%%%"/*configuration["jwt : Key"]!*/);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userId", user.userId.ToString()) ,
                 new Claim("userName",user.emailId.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
