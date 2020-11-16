using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<Employee, EmployeeServiceModel>()
                .ForPath(x => x.Manager.Manager, opts => opts.Ignore())
                .ForPath(x => x.Department.Manager, opts => opts.Ignore())
                .ReverseMap();
        }
    }
}
