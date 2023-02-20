using Models;
using System.Text.RegularExpressions;
using ViewModel;

namespace Services
{
    public interface IUserService
    {
        public bool IsValidLogin(string emailId, string password);
        public bool IsValidSignup(UserView user);
    }
}