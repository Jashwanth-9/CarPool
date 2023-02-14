using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class RideDetails
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("User")]
        public int offeredUserId { get; set; }
        public string? fromLocation { get; set; }
        public string? toLocation { get; set; }
        public string? date { get; set; }
        public string? rideTime { get; set; }
        public int price { get; set; }
        public string? stops { get; set; }
        public string? availableSeats { get; set; }
    }
}
