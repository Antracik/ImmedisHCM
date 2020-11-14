using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class LocationMapping : Profile
    {
        public LocationMapping()
        {
            CreateMap<Location, LocationServiceModel>().ReverseMap();
        }
    }
}
