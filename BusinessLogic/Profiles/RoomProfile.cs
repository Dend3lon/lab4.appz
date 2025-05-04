using AutoMapper;
using BusinessLogic.BusinessModels;
using DomainData.Models;

namespace BusinessLogic.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomBusinessModel, Room>();
            CreateMap<Room, RoomBusinessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.RoomNumber))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity));
        }
    }
}
