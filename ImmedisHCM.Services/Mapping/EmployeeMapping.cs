using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<Employee, EmployeeServiceModel>().ReverseMap();
        }
    }
}
