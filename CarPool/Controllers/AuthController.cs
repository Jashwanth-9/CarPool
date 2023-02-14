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


namespace CarPool.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        IUserService user;
        public AuthController(IUserService user)
        {
            this.user= user;
        }
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(User signup)
        {
            if (user.IsValidSignup(signup)) {
                return Ok(signup);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string emailId,string password)
        {
            
            if (user.IsValidLogin(emailId,password))
            {

                return Ok();
            }
            return BadRequest();
        }

    }
}
