/*using Models;*/
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingService : IBookingService
    {
        DBCarContext carContext;
        public BookingService(DBCarContext context)
        {
            carContext = context;
        }

        private List<string> getStops(string fromLocation,string stops,string toLocation)
        {
            List<string> locations = new List<string>();
            locations.Add($"{fromLocation}");
            locations.AddRange(stops.Split(',').ToList());
            locations.Add($"{toLocation}");
            return locations;
        }

        private List<int> getSeats(string availableSeats)
        {
            List<string> available = availableSeats.Split(',').ToList();
            List<int> seats = available.Select(a => int.Parse(a)).ToList();
            return seats;
        }
        public List<RideDetails> GetMatchingRides(RideDetails ride)

        {
            List<RideDetails> rides = carContext.RideDetails.Where(r => r.rideTime == ride.rideTime && r.date == ride.date).ToList();
            List<RideDetails> availableRides=new List<RideDetails>();
            foreach (RideDetails available in rides)
            {
                List<string> locations = getStops(available.fromLocation!, available.stops!, available.toLocation!);
                List<int> seats = getSeats(available.availableSeats!);
                int min = int.MaxValue;
                if (locations.Contains(ride.fromLocation) && locations.Contains(ride.toLocation))
                {
                    int n = Math.Abs(locations.IndexOf(ride.toLocation) - locations.IndexOf(ride.fromLocation));
                    int p = (available.price / locations.Count());
                    
                    available.fromLocation = ride.fromLocation;
                    available.toLocation = ride.toLocation;
                    available.price = p*n;
                    
                    for (int i = locations.IndexOf(ride.fromLocation); i <= locations.IndexOf(ride.toLocation); i++)
                    {
                        if (seats[i] < min)
                        {
                            min = seats[i];
                            available.availableSeats = Convert.ToString(min);
                        }
                    }
                }
                if (min!=int.MaxValue && min!=0)
                {
                    availableRides.Add(available);
                }
            }
            return availableRides;
        }

        private void Book(RideDetails ride)
        {
            RideDetails offered = carContext.RideDetails.Where(r => r.id == ride.id).First();
            List<string> locations = getStops(offered.fromLocation, offered.stops, offered.toLocation);
            List<int> availableSeats = getSeats(offered.availableSeats!);
            offered.availableSeats = "";
            if(locations.Count()> 0)
            {
                for (int i = 0; i < locations.Count(); i++)
                {
                    if (i >= locations.IndexOf(ride.fromLocation!) && i < locations.IndexOf(ride.toLocation!))
                    {
                        availableSeats[i] = availableSeats[i] - 1;
                    }
                    offered.availableSeats = offered.availableSeats + Convert.ToString(availableSeats[i]) + ",";
                }
                int index = offered.availableSeats.LastIndexOf(",");
                offered.availableSeats = offered.availableSeats.Remove(index);
            }
            Console.WriteLine(offered);
            carContext.Update(offered);
            carContext.SaveChanges();
        }

        public void BookRide(RideDetails ride)
        {
            BookRide new_booking = new BookRide();
            new_booking.userId = UserService.userId;
            new_booking.date = ride.date;
            new_booking.rideTime = ride.rideTime;
            new_booking.rideId = ride.id;
/*            new_booking.fromLocation=ride.fromLocation;
            new_booking.toLocation=ride.toLocation;*/
            new_booking.price = ride.price;
            new_booking.offeredUserId=ride.offeredUserId;
            carContext.Add(new_booking);
            carContext.SaveChanges();
            Book(ride);
        }

        public List<BookRide> GetBookedRides()
        {
            List<BookRide> bookedRides = carContext.BookedRides.ToList();
            return bookedRides;
        }
    }
}
