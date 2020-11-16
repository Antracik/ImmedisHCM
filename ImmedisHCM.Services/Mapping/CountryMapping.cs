using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;
using NHibernate;

namespace ImmedisHCM.Services.Mapping
{
    public class CountryMapping : Profile
    {
        public CountryMapping()
        {
            CreateMap<Country, CountryServiceModel>()
                .ForMember(dest => dest.Cities, opts => opts.PreCondition(src => NHibernateUtil.IsInitialized(src.Cities)))
                .ReverseMap();
        }
    }
}
