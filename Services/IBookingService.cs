using Models;
using ViewModel;

namespace Services
{
    public interface IBookingService
    {   
        public List<BookRide> GetMatchingRides(BookRide ride);
        public BookRide BookRide(BookRide ride);

        public List<BookRide> GetBookedRides();
    }
}