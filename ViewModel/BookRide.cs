namespace ViewModel
{
    public class BookRide
    {
        public int rideId { get; set; }
        public int offeredUserId { get; set; }
        public string? fromLocation { get; set; }
        public string? toLocation { get; set; }
        public string? date { get; set; }
        public string? rideTime { get; set; }
        public int price { get; set; }
    }
}
