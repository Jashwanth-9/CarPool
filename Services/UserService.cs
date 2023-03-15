using System.Globalization;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Extensions;
using ViewModel;

namespace Services
{
    public class UserService : IUserService
    {
        DBCarContext carContext;
        IMapper mapper;
        public static int UserId { get; set; }
        public UserService(DBCarContext context, IMapper mapper) {
            carContext = context;
            this.mapper = mapper;
        }


        public bool IsValidLogin(string emailId, string password)
        {
            try
            {
                User data = carContext.Users.Where(u => u.EmailId == emailId).First();
                if (data.EmailId == emailId && data.Password.DecryptString() == password)
                {
                    UserId = data.UserId;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValidSignup(UserView user)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            if (Regex.IsMatch(user.EmailId!, regex, RegexOptions.IgnoreCase) && user.Password != null)
            {
                Signup(user);
                return true;
            }
            return false;
        }

        public void Signup(UserView user)
        {
            user.Password = user.Password.EncryptString();
            /*User new_user = new User();
            new_user.emailId = user.emailId;
            new_user.password = user.password.EncryptString();
            new_user.firstName = user.firstName;
            new_user.lastName = user.lastName;
            new_user.mobileNumber = user.mobileNumber;*/
            var User = mapper.Map<User>(user);
            carContext.Add(User);
            carContext.SaveChanges();
        }
    }
}