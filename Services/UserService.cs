using System.Text.RegularExpressions;
using Models;

namespace Services
{
    public class UserService : IUserService
    {
        DBCarContext carContext;
        public static int userId { get; set; }
        public UserService(DBCarContext context) {
            carContext = context;
        }
       
        public bool IsValidLogin(string emailId, string password)
        {
            try
            {
                User data = carContext.Users.Where(u => u.emailId == emailId).First();
                if (data.emailId == emailId && data.password == password)
                {
                    userId = data.userId;
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool IsValidSignup(User user)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            if (Regex.IsMatch(user.emailId!, regex, RegexOptions.IgnoreCase) && user.password != null)
            {
                Signup(user);
                return true;
            }
            return false;
        }

        public void Signup(User user)
        {
            User new_user = new User();
            new_user.emailId = user.emailId;
            new_user.password = user.password;
            new_user.firstName = user.firstName;
            new_user.lastName = user.lastName;
            new_user.mobileNumber = user.mobileNumber;
            carContext.Add(new_user);
            carContext.SaveChanges();
        }
    }
}