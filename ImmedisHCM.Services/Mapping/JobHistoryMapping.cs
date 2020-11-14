using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class JobHistoryMapping : Profile
    {
        public JobHistoryMapping()
        {
            CreateMap<JobHistory, JobHistoryServiceModel>().ReverseMap();
        }
    }
}
