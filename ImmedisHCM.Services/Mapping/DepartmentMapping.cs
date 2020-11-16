using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;
using NHibernate;

namespace ImmedisHCM.Services.Mapping
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, DepartmentServiceModel>()
                .ForMember(dest => dest.Employees, opts => opts.PreCondition(src => NHibernateUtil.IsInitialized(src.Employees)))
                .ReverseMap();
        }
    }
}
