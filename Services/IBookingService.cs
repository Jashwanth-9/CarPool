/*using Models;*/
using Models;

namespace Services
{
    public interface IBookingService
    {
        public List<RideDetails> GetMatchingRides(RideDetails ride);
        public void BookRide(int rideId);

        public List<BookRide> GetBookedRides();
    }
}