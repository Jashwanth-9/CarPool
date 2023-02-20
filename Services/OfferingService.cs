using AutoMapper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace Services
{
    public class OfferingService : IOfferingService
    {
        DBCarContext carContext;
        IMapper mapper;
        public OfferingService(DBCarContext context, IUserService user, IMapper mapper)
        {
            carContext = context;
            this.mapper = mapper;
        }
        public OfferRide OfferRide(OfferRide ride)
        {
            /*RideDetails new_ride = new RideDetails();
            new_ride.price = ride.price;
            new_ride.rideTime = ride.rideTime;
            new_ride.date = ride.date;
            new_ride.availableSeats = ride.availableSeats;
            new_ride.toLocation = ride.toLocation;
            new_ride.fromLocation = ride.fromLocation;
            new_ride.stops = ride.stops;*/
            RideDetails new_ride = mapper.Map<RideDetails>(ride);
            
            if (ride.stops == "")
            {
                new_ride.availableSeats += $",{ride.availableSeats}";
            }
            else
            {
                int length = ride.stops!.Split(",").Count()+1;
                for (int i = 0; i < length; i++)
                {
                    new_ride.availableSeats += $",{ride.availableSeats}";
                }
            }
            new_ride.offeredUserId = UserService.userId;
            carContext.Add(new_ride);
            carContext.SaveChanges();
            return ride;
        }


        public List<BookRide> GetOfferedRides()
        {
            List<RideDetails> offeredRides = carContext.RideDetails.Where(r => r.offeredUserId == UserService.userId).ToList();
            List<BookRide> offered= new List<BookRide>();
            foreach(RideDetails ride in offeredRides)
            {
                offered.Add(mapper.Map<BookRide>(ride));
            }
            return offered;
        }
    }
}
