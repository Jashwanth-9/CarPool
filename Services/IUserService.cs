using Models;
using System.Text.RegularExpressions;

namespace Services
{
    public interface IUserService
    {
        public int userId { get; set; }
        public bool IsValidLogin(string emailId, string password);
        public bool IsValidSignup(User user);

        public void Signup(User user);
    }
}