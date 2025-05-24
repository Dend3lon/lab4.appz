using AutoMapper;
using BusinessModels;
using DomainData.Models;

namespace AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomBusinessModel>().ReverseMap();

            CreateMap<Activity, ActivityBusinessModel>().ReverseMap();

            CreateMap<Booking, BookingBusinessModel>().ReverseMap();
        }
    }

}
