using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class AttendanceHistoryMapping : Profile
    {
        public AttendanceHistoryMapping()
        {
            CreateMap<AttendanceHistory, AttendanceHistoryServiceModel>().ReverseMap();
        }
    }
}
