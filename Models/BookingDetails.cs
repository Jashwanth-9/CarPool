using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class BookingDetails
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey("Ride")]
        public int rideId { get; set; }
        public int offeredUserId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        public string? fromLocation { get; set; }
        public string? toLocation { get; set; } 
        public string? date { get; set; }
        public string? rideTime { get; set; }
        public int price { get; set; }
    }
}