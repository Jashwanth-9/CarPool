using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;
using ViewModel;
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
        ITokenService tokenService;
        public AuthController(IUserService userService, ITokenService tokenService)
        {
            this.userService= userService;
            this.tokenService= tokenService;
        }
        [HttpPost]
        [Route("SignUp")]
        /*public IActionResult SignUp(User signup)
        {
            if (userService.IsValidSignup(signup)) {
                return Ok(signup);
            }
            return BadRequest();
        }*/
        public IActionResult SignUp(UserView signup)
        {
            if (userService.IsValidSignup(signup))
            {
                return Ok(signup);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserView user)
        {
            if (userService.IsValidLogin(user.EmailId!,user.Password!))
            {
                var token = tokenService.GenerateJwtToken(user);
                return Ok(token);
            }
            return NotFound("No user found with specified emailId and Password");
        }
    }
}
