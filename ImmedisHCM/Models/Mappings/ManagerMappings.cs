using AutoMapper;
using ImmedisHCM.Services.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models.Mappings
{
    public class ManagerMappings : Profile
    {
        public ManagerMappings()
        {
            CreateMap<EmployeeServiceModel, EmployeesViewModel>()
                .ForMember(x => x.Name, opts => opts.MapFrom((src, dst) =>
                {
                    return $"{src.FirstName} {src.LastName}";
                }
                ))
                .ForMember(x => x.Salary, opts => opts.MapFrom(x => x.Salary.Amount))
                .ForMember(x => x.Currency, opts => opts.MapFrom(x => x.Salary.Currency.Name))
                .ForMember(x => x.SalaryTypeName, opts => opts.MapFrom(x => x.Salary.SalaryType.Name))
                .ForMember(x => x.JobName, opts => opts.MapFrom(x => x.Job.Name))
                .ForMember(x => x.ScheduleName, opts => opts.MapFrom(x => x.Job.ScheduleType.Name));
        }
    }
}
