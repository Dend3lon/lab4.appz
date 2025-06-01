using AutoMapper;
using BusinessModels;
using DomainData.Models;
using DtoModels;

namespace AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomBusinessModel>().ReverseMap();
            CreateMap<RoomBusinessModel, RoomDto>().ReverseMap();

            CreateMap<Activity, ActivityBusinessModel>().ReverseMap();
            CreateMap<ActivityBusinessModel, ActivityDto>().ReverseMap();

            CreateMap<Booking, BookingBusinessModel>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber))
                .ForMember(dest => dest.ActivityIds, opt => opt.MapFrom(src => src.Activities.Select(a => a.Id)))
                .ReverseMap();
            CreateMap<BookingBusinessModel, BookingDto>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))
                .ForMember(dest => dest.ActivityIds, opt => opt.MapFrom(src => src.ActivityIds))
                .ReverseMap()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))
                .ForMember(dest => dest.ActivityIds, opt => opt.MapFrom(src => src.ActivityIds));

        }
    }

}
