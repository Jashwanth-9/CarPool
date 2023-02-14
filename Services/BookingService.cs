/*using Models;*/
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingService : IBookingService
    {
        DBCarContext carContext;
        IUserService user;
        public int userId;
        public BookingService(DBCarContext context, IUserService userService)
        {
            carContext = context;
            user = userService;
            userId = userService.userId;
        }
        public List<RideDetails> GetMatchingRides(RideDetails ride)
        {
            /*List<Ride> availableRides = carContext.Rides.Where(r => r.inTime == ride.inTime && r.outTime == ride.outTime && r.date == ride.date && r.toLocation == ride.toLocation && r.fromLocation == ride.fromLocation && r.availableSeats > 0).ToList();*/
            List<RideDetails> availableRides = carContext.RideDetails.Where(r => r.rideTime == ride.rideTime && r.date == ride.date).ToList();
            foreach (RideDetails available in availableRides)
            {
                List<string> locations = new List<string>();
                locations.Add($"{available.fromLocation}");
                locations.AddRange(available.stops.Split(',').ToList());
                locations.Add($"{available.toLocation}");
                if(locations.Contains(ride.fromLocation) && locations.Contains(ride.toLocation))
                {
                    int n =Math.Abs(locations.IndexOf(ride.toLocation) - locations.IndexOf(ride.fromLocation));
                    /*foreach(available.availableSeats.Split(',').ToList();*/

                }
            }
            return availableRides;
        }

        public void BookRide(int rideId)
        {
            BookRide new_booking = new BookRide();
            var ride = carContext.RideDetails.Where(r => r.id == rideId).First();
            new_booking.userId = userId;
            new_booking.date = ride.date;
            new_booking.rideTime = ride.rideTime;
            new_booking.rideId = ride.id;
            carContext.Add(new_booking);
            carContext.SaveChanges();
        }

        public List<BookRide> GetBookedRides()
        {
            List<BookRide> bookedRides = carContext.BookedRides.ToList();
            return bookedRides;
        }
    }
}
