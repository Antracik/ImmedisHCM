using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, DepartmentServiceModel>().ReverseMap();
        }
    }
}
