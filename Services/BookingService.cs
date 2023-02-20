using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModel;
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
        IMapper mapper;
        public BookingService(DBCarContext context, IMapper mapper)
        {
            carContext = context;
            this.mapper = mapper;
        }

        private List<string> getStops(string fromLocation,string stops,string toLocation)
        {
            List<string> locations = new List<string>();
            locations.Add($"{fromLocation}");
            if(stops!= "")
            {
                locations.AddRange(stops.Split(',').ToList());
            }
            locations.Add($"{toLocation}");
            return locations;
        }

        private List<int> getSeats(string availableSeats)
        {
            List<string> available = availableSeats.Split(',').ToList();
            List<int> seats = available.Select(a => int.Parse(a)).ToList();
            return seats;
        }
        public List<BookRide> GetMatchingRides(BookRide ride)
        {
            try
            {
                List<RideDetails> rides = carContext.RideDetails.Where(r => r.rideTime == ride.rideTime && r.date == ride.date).ToList();
                List<BookRide> availableRides = new List<BookRide>();
                foreach (RideDetails available in rides)
                {
                    List<string> locations = getStops(available.fromLocation!, available.stops!, available.toLocation!);
                    List<int> seats = getSeats(available.availableSeats!);
                    int minimumSeats = int.MaxValue;
                    if (locations.Contains(ride.fromLocation!) && locations.Contains(ride.toLocation!))
                    {
                        int numberOfLocations = Math.Abs(locations.IndexOf(ride.toLocation!) - locations.IndexOf(ride.fromLocation!));
                        int priceOfEachStop = (available.price / locations.Count());

                        available.fromLocation = ride.fromLocation;
                        available.toLocation = ride.toLocation;
                        available.price = priceOfEachStop * numberOfLocations;

                        for (int i = locations.IndexOf(ride.fromLocation!); i <= locations.IndexOf(ride.toLocation!); i++)
                        {
                            if (seats[i] < minimumSeats)
                            {
                                minimumSeats = seats[i];
                                available.availableSeats = Convert.ToString(minimumSeats);
                            }
                        }
                    }
                    if (minimumSeats != int.MaxValue && minimumSeats >= 0)
                    {
                        BookRide new_match = mapper.Map<BookRide>(available);
                        availableRides.Add(new_match);
                    }
                }
                return availableRides;
        }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }

        private bool Book(BookingDetails ride)
        {
            RideDetails offered = new RideDetails();
            try
            {
                offered = carContext.RideDetails.Where(r => r.id == ride.rideId).First();
                if (offered == null)
                {
                    return false;
                }
                List<string> locations = getStops(offered.fromLocation!, offered.stops!, offered.toLocation!);
                List<int> availableSeats = getSeats(offered.availableSeats!);
                offered.availableSeats = "";
                if (locations.Count() > 0)
                {
                    for (int i = 0; i < availableSeats.Count(); i++)
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
                carContext.Update(offered);
                carContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public BookRide BookRide(BookRide ride)
        {
            /*BookingDetails new_booking = new BookingDetails();*//*
            new_booking.userId = UserService.userId;
            new_booking.date = ride.date;
            new_booking.rideTime = ride.rideTime;
            new_booking.rideId = ride.id;
            new_booking.fromLocation = ride.fromLocation;
            new_booking.toLocation = ride.toLocation;
            new_booking.price = ride.price;
            new_booking.offeredUserId = ride.offeredUserId;*/
            BookingDetails new_booking = mapper.Map<BookingDetails>(ride);
            new_booking.userId = UserService.userId;
            if (!Book(new_booking))
            {
                return null;
            };
            
            carContext.Add(new_booking);
            carContext.SaveChanges();

            return (ride);
        }

        public List<BookRide> GetBookedRides()
        {
            List<BookingDetails> bookedRides_ = carContext.BookedRides.Where(b => b.userId==UserService.userId).ToList();
            List<BookRide> bookedRides = new List<BookRide>();
            foreach(BookingDetails ride in bookedRides_)
            {
                bookedRides.Add(mapper.Map<BookRide>(ride));
            }
            return bookedRides;
        }
    }
}
