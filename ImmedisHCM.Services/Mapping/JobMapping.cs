using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class JobMapping : Profile
    {
        public JobMapping()
        {
            CreateMap<Job, JobServiceModel>().ReverseMap();
        }
    }
}
