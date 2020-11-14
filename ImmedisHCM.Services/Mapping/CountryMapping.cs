using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class CountryMapping : Profile
    {
        public CountryMapping()
        {
            CreateMap<Country, CountryServiceModel>().ReverseMap();
        }
    }
}
