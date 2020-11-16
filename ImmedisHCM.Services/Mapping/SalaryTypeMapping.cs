using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class SalaryTypeMapping : Profile
    {
        public SalaryTypeMapping()
        {
            CreateMap<SalaryType, SalaryTypeServiceModel>().ReverseMap();
        }
    }
}
