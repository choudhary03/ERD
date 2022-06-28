using AutoMapper;
using ERD.Models;
using ERD.ViewModels;
using Refreshment_Dashboard.Models;

namespace ERD.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Enrollment, EnrollmentViewModelAutomapper>().ReverseMap();
            CreateMap<Enrollment, EnrollmentViewModelAutomapper>()
                .ForMember(d => d.Firstname, s => s.MapFrom(src => src.Employee.Firstname))
                //.ForMember(d => d.ActivityName, s => s.MapFrom(src => string.Join(", ",src.Activity.ActivityName.ToList().Select(x=>x)) ))
                .ForMember(d => d.ActivityList, s => s.MapFrom(src => src.Activity.Name.ToList()))
                .ReverseMap();
        }
    }
}
