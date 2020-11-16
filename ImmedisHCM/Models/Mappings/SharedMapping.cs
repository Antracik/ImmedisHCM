using AutoMapper;
using ImmedisHCM.Services.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models.Mappings
{
    public class SharedMapping : Profile
    {
        public SharedMapping()
        {
            CreateMap<CityServiceModel, CityViewModel>().ReverseMap();
            CreateMap<CountryServiceModel, CountryViewModel>().ReverseMap();
            CreateMap<LocationServiceModel, LocationViewModel>().ReverseMap();
        }
    }
}
