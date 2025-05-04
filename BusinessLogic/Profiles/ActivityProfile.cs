using AutoMapper;
using BusinessLogic.BusinessModels;
using DomainData.Models;

namespace BusinessLogic.Profiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile() 
        {
            CreateMap<ActivityBusinessModel, Activity>();
            CreateMap<Activity, ActivityBusinessModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
