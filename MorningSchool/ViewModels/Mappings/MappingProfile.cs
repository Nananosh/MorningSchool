using AutoMapper;
using MorningSchool.ViewModels.Admin;
using MorningSchool.ViewModels.Schedule;

namespace MorningSchool.ViewModels.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClassViewModel, Models.Class>().ReverseMap();
            CreateMap<ScheduleViewModel, Models.Schedule>().ReverseMap();
        }
    }
}