using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class CityMapping : Profile
    {
        public CityMapping()
        {
            CreateMap<City, CityServiceModel>().ReverseMap();
        }
    }
}
