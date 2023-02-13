using System.ComponentModel.DataAnnotations;

namespace Models
{
    
    public class User
    {
        [Key]
        public int userId { get; set; }
        public string? emailId { get; set; }
        public string? password { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public int mobileNumber { get; set; }

    }
}
