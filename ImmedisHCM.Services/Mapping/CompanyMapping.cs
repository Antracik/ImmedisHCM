using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class CompanyMapping : Profile
    {
        public CompanyMapping()
        {
            CreateMap<Company, CompanyServiceMolde>().ReverseMap();
        }
    }
}
