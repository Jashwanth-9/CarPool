using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModel;

namespace Services
{
    public interface ITokenService
    {
        public string GenerateJwtToken(UserView user);
    }
}