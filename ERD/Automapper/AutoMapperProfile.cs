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
        }
    }
}
