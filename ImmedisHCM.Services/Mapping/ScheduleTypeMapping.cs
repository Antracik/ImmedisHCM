using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class ScheduleTypeMapping : Profile
    {
        public ScheduleTypeMapping()
        {
            CreateMap<ScheduleType, ScheduleTypeServiceModel>().ReverseMap();
        }
    }
}
