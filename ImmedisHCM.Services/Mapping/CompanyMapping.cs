using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;
using NHibernate;

namespace ImmedisHCM.Services.Mapping
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping()
        {
            CreateMap<Company, CompanyServiceModel>()
                .ForMember(dest => dest.Departments, opts => opts.PreCondition(src => NHibernateUtil.IsInitialized(src.Departments)))
                .ReverseMap();
        }
    }
}
