using CarPool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Services
{
    public class OfferingService
    {
        DBCarContext carContext;
        UserService userService;
        public int userId;

        public OfferingService()
        {
            carContext = new DBCarContext();
            userService = new UserService();
            userId = userService.userId;

        }
        public void OfferRide(Ride ride)
        {
            Ride new_ride = new Ride();
            new_ride.price = ride.price;
            new_ride.inTime = ride.inTime;
            new_ride.outTime = ride.outTime;
            new_ride.date = ride.date;
            new_ride.availableSeats = ride.availableSeats;
            new_ride.toLocation = ride.toLocation;
            new_ride.fromLocation = ride.fromLocation;
            new_ride.stop = ride.stop;
            new_ride.userId = userId;
            carContext.Add(new_ride);
            carContext.SaveChanges();
        }

        public List<Ride> GetOfferedRides()
        {
            List<Ride> offeredRides = carContext.Rides.ToList();
            return offeredRides;
        }
    }
}
