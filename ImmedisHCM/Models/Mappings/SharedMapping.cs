using AutoMapper;
using ImmedisHCM.Services.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models.Mappings
{
    public class SharedMapping : Profile
    {
        public SharedMapping()
        {
            CreateMap<CityServiceModel, CityViewModel>().ReverseMap();
            CreateMap<CountryServiceModel, CountryViewModel>().ReverseMap();
            CreateMap<LocationServiceModel, LocationViewModel>().ReverseMap();
            CreateMap<DepartmentServiceModel, DepartmentViewModel>().ReverseMap();
            CreateMap<JobServiceModel, JobViewModel>().ReverseMap();
            CreateMap<SalaryTypeServiceModel, SalaryTypeViewModel>().ReverseMap();
            CreateMap<CurrencyServiceModel, CurrencyViewModel>().ReverseMap();
            CreateMap<CompanyServiceModel, CompanyViewModel>().ReverseMap();
            CreateMap<ScheduleTypeServiceModel, ScheduleTypeViewModel>();

            CreateMap<SalaryServiceModel, UpdateEmployeeSalaryViewModel>()
                .ForMember(x => x.EmployeeName, opts => opts.MapFrom((src, dsrt) =>
                {
                    return $"{src.Employee.FirstName} {src.Employee.LastName}";
                }))
                .ForMember(x => x.EmployeeEmail, opts => opts.MapFrom(x => x.Employee.Email))
                .ForMember(x => x.SalaryTypeId, opts => opts.MapFrom(x => x.SalaryType.Id))
                .ForMember(x => x.CurrencyId, opts => opts.MapFrom(x => x.Currency.Id));

            
        }
    }
}
