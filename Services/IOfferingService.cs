using Models;
using ViewModel;

namespace Services
{
    public interface IOfferingService
    {
        public OfferRide OfferRide(OfferRide ride);

        public List<BookRide> GetOfferedRides();
    }
}