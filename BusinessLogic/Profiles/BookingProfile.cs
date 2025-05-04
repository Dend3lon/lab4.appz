using AutoMapper;
using BusinessLogic.BusinessModels;
using DomainData.Models;

namespace BusinessLogic.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingBusinessModel>()
                .ForMember(dest => dest.ActivityIds, opt =>
                    opt.MapFrom(src => src.Activities.Select(a => a.Id).ToList()))
                .ForMember(dest => dest.RoomId, opt =>
                    opt.MapFrom(src => src.RoomId));

            CreateMap<BookingBusinessModel, Booking>()
                .ForMember(dest => dest.Activities, opt => opt.Ignore()) 
                .ForMember(dest => dest.Id, opt => opt.Ignore());     
        }
    }
}
