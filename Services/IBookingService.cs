/*using Models;*/
using Models;

namespace Services
{
    public interface IBookingService
    {
        public List<RideDetails> GetMatchingRides(RideDetails ride);
        public void BookRide(RideDetails rideId);

        public List<BookRide> GetBookedRides();
    }
}