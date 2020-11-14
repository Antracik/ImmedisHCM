using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{ 
    public class CurrencyMapping : Profile
    {
        public CurrencyMapping()
        {
            CreateMap<Currency, CurrencyServiceModel>().ReverseMap();
        }
    }
}
