using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;
using NHibernate;

namespace ImmedisHCM.Services.Mapping
{
    public class SalaryTypeMapping : Profile
    {
        public SalaryTypeMapping()
        {
            CreateMap<SalaryType, SalaryTypeServiceModel>()
                .ForMember(dest => dest.Salaries, opts => opts.PreCondition(src => NHibernateUtil.IsInitialized(src.Salaries)))
                .ReverseMap();
        }
    }
}
