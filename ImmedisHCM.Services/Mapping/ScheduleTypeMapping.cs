using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;
using NHibernate;

namespace ImmedisHCM.Services.Mapping
{
    public class ScheduleTypeMapping : Profile
    {
        public ScheduleTypeMapping()
        {
            CreateMap<ScheduleType, ScheduleTypeServiceModel>()
                .ForMember(dest => dest.Jobs, opts => opts.PreCondition(src => NHibernateUtil.IsInitialized(src.Jobs)))
                .ReverseMap();
        }
    }
}
