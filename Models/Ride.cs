using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Ride
    {
        [Key]
        public int rideId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public string? fromLocation { get; set; }
        public string? toLocation { get; set; }
        public string? date { get; set; }
        public string? inTime { get; set; }
        public string? outTime { get; set; }
        public int price { get; set; }
        public string? stop { get; set; }
        public int availableSeats { get; set; }
    }
}
