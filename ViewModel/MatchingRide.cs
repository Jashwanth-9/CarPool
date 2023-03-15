using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MatchingRide
    {
        public int RideId { get; set; }
        public int offeredUserId { get; set; }
        public string? RideUserName { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public string? Date { get; set; }
        public string? RideTime { get; set; }
        public int Price { get; set; }
        public string? AvailableSeats { get; set; }
    }
}
