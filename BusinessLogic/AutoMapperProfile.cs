using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.BusinessModels;
using DomainData.Models;

namespace BusinessLogic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Room, RoomBusinessModel>().ReverseMap();

            CreateMap<Activity, ActivityBusinessModel>().ReverseMap();

            CreateMap<Booking, BookingBusinessModel>()
                .ForMember(dest => dest.ActivityIds,
                    opt => opt.MapFrom(src => src.Activities.Select(a => a.Id)))
                .ReverseMap()
                .ForPath(dest => dest.Activities, opt =>
                    opt.MapFrom(src => src.ActivityIds.Select(id => new Activity { Id = id })));
        }
    }

}
