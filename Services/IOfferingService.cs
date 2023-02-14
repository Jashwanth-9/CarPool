using Models;

namespace Services
{
    public interface IOfferingService
    {
        public void OfferRide(RideDetails ride);

        public List<RideDetails> GetOfferedRides();
    }
}