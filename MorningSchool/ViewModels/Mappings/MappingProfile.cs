using AutoMapper;
using MorningSchool.Models;

namespace MorningSchool.ViewModels.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClassViewModel, Class>().ReverseMap();
            CreateMap<CabinetViewModel, Cabinet>().ReverseMap();
            CreateMap<ClassroomTeacherViewModel, ClassroomTeacher>().ReverseMap();
            CreateMap<EventViewModel, Event>().ReverseMap();
            CreateMap<ThemeViewModel, Theme>().ReverseMap();
            CreateMap<EventResultViewModel, EventResult>().ReverseMap();
            CreateMap<EventSubscriptionViewModel, EventSubscription>().ReverseMap();
        }
    }
}