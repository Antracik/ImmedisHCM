using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class EmergencyContactMapping : Profile
    {
        public EmergencyContactMapping()
        {
            CreateMap<EmergencyContact, EmergencyContactServiceModel>()
                .ForSourceMember(x => x.Employee, opts => opts.DoNotValidate()).ReverseMap();
        }
    }
}
