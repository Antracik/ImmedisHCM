using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Services.Models.Core;

namespace ImmedisHCM.Services.Mapping
{
    public class SalaryMapping : Profile

    {
        public SalaryMapping()
        {
            CreateMap<Salary, SalaryServiceModel>().ReverseMap();
        }
    }
}
