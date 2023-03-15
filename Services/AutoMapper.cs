using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using ViewModel;

namespace Services
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<UserView, User>().ReverseMap();
            CreateMap<BookRide, BookingDetails>().ReverseMap();
            CreateMap<OfferRide, RideDetails>().ReverseMap();
            CreateMap<BookRide, RideDetails>().ForMember(dest => dest.Id , act => act.MapFrom(src=> src.RideId)).ReverseMap();
            CreateMap<BookRide, MatchingRide>().ReverseMap();
        }
    }
}
