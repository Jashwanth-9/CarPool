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
        public List<MatchingRide> GetMatchingRides(BookRide ride)
        {
            /*try
            {
                List<RideDetails> rides = carContext.RideDetails.Where(r => r.RideTime == ride.RideTime && r.Date == ride.Date).ToList();
                List<BookRide> availableRides = new List<BookRide>();
                List<MatchingRide> availableRides = new List<MatchingRide>();
                foreach (RideDetails available in rides)
                {
                    List<string> locations = getStops(available.FromLocation!, available.Stops!, available.ToLocation!);
                    List<int> seats = getSeats(available.AvailableSeats!);
                    int minimumSeats = int.MaxValue;
                    if (locations.Contains(ride.FromLocation!) && locations.Contains(ride.ToLocation!))
                    {
                        int numberOfLocations = Math.Abs(locations.IndexOf(ride.ToLocation!) - locations.IndexOf(ride.FromLocation!));
                        int priceOfEachStop = (available.Price / locations.Count());

                        available.FromLocation = ride.FromLocation;
                        available.ToLocation = ride.ToLocation;
                        available.Price = priceOfEachStop * numberOfLocations;

                        for (int i = locations.IndexOf(ride.FromLocation!); i <= locations.IndexOf(ride.ToLocation!); i++)
                        {
                            if (seats[i] < minimumSeats)
                            {
                                minimumSeats = seats[i];
                                available.AvailableSeats = Convert.ToString(minimumSeats);
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
        }*/

            try
            {
                List<RideDetails> rides = carContext.RideDetails.Where(r => r.RideTime == ride.RideTime && r.Date == ride.Date).ToList();
                /*List<BookRide> availableRides = new List<BookRide>();*/
                List<MatchingRide> availableRides = new List<MatchingRide>();
                foreach (RideDetails available in rides)
                {
                    List<string> locations = getStops(available.FromLocation!, available.Stops!, available.ToLocation!);
                    List<int> seats = getSeats(available.AvailableSeats!);
                    int minimumSeats = int.MaxValue;
                    if (locations.Contains(ride.FromLocation!) && locations.Contains(ride.ToLocation!))
                    {
                        int numberOfLocations = Math.Abs(locations.IndexOf(ride.ToLocation!) - locations.IndexOf(ride.FromLocation!));
                        int priceOfEachStop = (available.Price / locations.Count());

                        available.FromLocation = ride.FromLocation;
                        available.ToLocation = ride.ToLocation;
                        available.Price = priceOfEachStop * numberOfLocations;

                        for (int i = locations.IndexOf(ride.FromLocation!); i <= locations.IndexOf(ride.ToLocation!); i++)
                        {
                            if (seats[i] < minimumSeats)
                            {
                                minimumSeats = seats[i];
                                available.AvailableSeats = Convert.ToString(minimumSeats);
                            }
                        }
                    }
                    if (minimumSeats != int.MaxValue && minimumSeats > 0)
                    {
                        BookRide new_match = mapper.Map<BookRide>(available);
                        MatchingRide new_matching = mapper.Map<MatchingRide>(new_match);
                        int Id = available.OfferedUserId;
                        User user = carContext.Users.Where(r => r.UserId == Id).First();
                        new_matching.RideUserName = user.FirstName+" " +user.LastName;
                        new_matching.AvailableSeats = available.AvailableSeats;
                        availableRides.Add(new_matching);
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
                offered = carContext.RideDetails.Where(r => r.Id == ride.RideId).First();
                if (offered == null)
                {
                    return false;
                }
                List<string> locations = getStops(offered.FromLocation!, offered.Stops!, offered.ToLocation!);
                List<int> availableSeats = getSeats(offered.AvailableSeats!);
                offered.AvailableSeats = "";
                if (locations.Count() > 0)
                {
                    for (int i = 0; i < availableSeats.Count(); i++)
                    {
                        if (i >= locations.IndexOf(ride.FromLocation!) && i < locations.IndexOf(ride.ToLocation!))
                        {
                            availableSeats[i] = availableSeats[i] - 1;
                        }
                        offered.AvailableSeats = offered.AvailableSeats + Convert.ToString(availableSeats[i]) + ",";
                    }
                    int index = offered.AvailableSeats.LastIndexOf(",");
                    offered.AvailableSeats = offered.AvailableSeats.Remove(index);
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
            BookingDetails new_booking = mapper.Map<BookingDetails>(ride);
            new_booking.UserId = UserService.UserId;
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
            List<BookingDetails> bookedRides_ = carContext.BookedRides.Where(b => b.UserId ==UserService.UserId).ToList();
            List<MatchingRide> matchingRides= new List<MatchingRide>();
            List<BookRide> bookedRides = new List<BookRide>();
            foreach(BookingDetails ride in bookedRides_)
            {
                bookedRides.Add(mapper.Map<BookRide>(ride));
            }
            return bookedRides;
        }
    }
}
