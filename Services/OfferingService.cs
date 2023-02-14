using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OfferingService : IOfferingService
    {
        DBCarContext carContext;
        public int userId;

        public OfferingService(DBCarContext context, IUserService user)
        {
            carContext = context;
            userId = user.userId;
        }
        public void OfferRide(RideDetails ride)
        {
            RideDetails new_ride = new RideDetails();
            new_ride.price = ride.price;
            new_ride.rideTime = ride.rideTime;
            new_ride.date = ride.date;
            new_ride.availableSeats = ride.availableSeats;
            new_ride.toLocation = ride.toLocation;
            new_ride.fromLocation = ride.fromLocation;
            new_ride.stops = ride.stops;
            int length = ride.stops.Split(",").Count() + 1;
            for (int i=0;i<length;i++)
            {
                new_ride.availableSeats += $",{ride.stops}";
            }
            new_ride.offeredUserId = userId;
            carContext.Add(new_ride);
            carContext.SaveChanges();
        }

        public List<RideDetails> GetOfferedRides()
        {
            List<RideDetails> offeredRides = carContext.RideDetails.ToList();
            return offeredRides;
        }
    }
}
