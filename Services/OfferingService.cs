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
            RideDetails new_ride = mapper.Map<RideDetails>(ride);
            
            if (ride.Stops == "")
            {
                new_ride.AvailableSeats += $",{ride.AvailableSeats}";
            }
            else
            {
                int length = ride.Stops!.Split(",").Count()+1;
                for (int i = 0; i < length; i++)
                {   
                    new_ride.AvailableSeats += $",{ride.AvailableSeats}";
                }
            }
            new_ride.OfferedUserId = UserService.UserId;
            carContext.Add(new_ride);
            carContext.SaveChanges();
            return ride;
        }


        public List<BookRide> GetOfferedRides()
        {
            List<RideDetails> offeredRides = carContext.RideDetails.Where(r => r.OfferedUserId == UserService.UserId).ToList();
            List<BookRide> offered= new List<BookRide>();
            foreach(RideDetails ride in offeredRides)
            {
                offered.Add(mapper.Map<BookRide>(ride));
            }
            return offered;
        }
    }
}
