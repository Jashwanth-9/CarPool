using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Models
{
    public class UserRide
    {
        [ForeignKey("Ride")]
        public int rideId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public string? date { get; set; }
        public string? inTime { get; set; }
        public string? outTime { get; set; }
        public int status { get; set; }
    }
}
