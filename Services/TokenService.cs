using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ViewModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class TokenService : ITokenService
    {
        IConfiguration configuration;
        public TokenService(IConfiguration _configuration) { 

            configuration= _configuration;
        }
        public string GenerateJwtToken(UserView user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(/*"asdv234234^&%&^%&^hjsdfb2%%%"*/configuration["jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] {
                 new Claim("userName",user.EmailId.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
