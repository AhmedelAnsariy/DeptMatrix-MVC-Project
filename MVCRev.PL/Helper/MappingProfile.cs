using AutoMapper;
using MVCRev.DAL.Models;
using MVCRev.PL.Models;

namespace MVCRev.PL.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();
        }
    }
}
