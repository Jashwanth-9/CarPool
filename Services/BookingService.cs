using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingService
    {
        DBCarContext carContext;
        UserService user;
        public int userId;
        public BookingService()
        {
            carContext = new DBCarContext();
            user= new UserService();
            userId = user.userId;
        }
        public List<Ride> GetMatchingRides(Ride ride)
        {
            List<Ride> availableRides = carContext.Rides.Where(r => r.inTime == ride.inTime && r.outTime == ride.outTime && r.date == ride.date && r.toLocation == ride.toLocation && r.fromLocation == ride.fromLocation && r.availableSeats > 0).ToList();
            return availableRides;
        }

        public void BookRide(int rideId)
        {
            UserRide new_booking = new UserRide();
            var ride = carContext.Rides.Where(r => r.rideId == rideId).First();
            new_booking.userId = userId;
            new_booking.date = ride.date;
            new_booking.inTime = ride.inTime;
            new_booking.outTime = ride.outTime;
            new_booking.rideId = ride.rideId;
            carContext.Add(new_booking);
            carContext.SaveChanges();
        }

        public List<UserRide> GetBookedRides()
        {
            List<UserRide> bookedRides = carContext.UserRides.ToList();
            return bookedRides;
        }
    }
}
