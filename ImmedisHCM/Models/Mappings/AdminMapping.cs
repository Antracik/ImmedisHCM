using AutoMapper;
using ImmedisHCM.Services.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models.Mappings
{
    public class AdminMapping : Profile
    {
        public AdminMapping()
        {
            CreateMap<CreateEmployeeViewModel, EmployeeServiceModel>()
                 .ForMember(x => x.Location, opt =>
                 {
                     opt.Condition(
                        s => s.Address != null
                     );
                     opt.MapFrom(s => s.Address);
                 });

            CreateMap<CreateEmployeeViewModel, CreateEmployeeViewModel>();

            CreateMap<EmployeeServiceModel, EmployeesAdminViewModel>()
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

            CreateMap<CompanyServiceModel, CompanyAdminViewModel>()
                .ForMember(x => x.DepartmentCount, opts => opts.MapFrom((src, dest) =>
                {
                    return src.Departments.Count;
                }));

            CreateMap<DepartmentServiceModel, DepartmentsAdminViewModel>()
                .ForMember(x => x.EmployeeCount, opts => opts.MapFrom((src, dest) =>
                {
                    return src.Employees.Count;
                }))
                .ForMember(x => x.CompanyName, opts => opts.MapFrom(x => x.Company.Name))
                .ForMember(x => x.Address1, opts => opts.MapFrom(x => x.Location.AddressLine1))
                .ForMember(x => x.Address2, opts => opts.MapFrom(x => x.Location.AddressLine2))
                .ForMember(x => x.PostalCode, opts => opts.MapFrom(x => x.Location.PostalCode))
                .ForMember(x => x.City, opts => opts.MapFrom(x => x.Location.City.Name))
                .ForMember(x => x.Country, opts => opts.MapFrom(x => x.Location.City.Country.Name))
                .ForMember(x => x.ManagerName, opts => opts.PreCondition(x => x.Manager != null))
                .ForMember(x => x.ManagerName, opts => opts.MapFrom((src, dst) =>
                {
                    return $"{src.Manager.FirstName} {src.Manager.LastName}";
                }));

            CreateMap<JobServiceModel, JobAdminViewModel>()
                .ForMember(x => x.ScheduleName, opts => opts.MapFrom(x => x.ScheduleType.Name))
                .ForMember(x => x.Hours, opts => opts.MapFrom(x => x.ScheduleType.Hours))
                .ForMember(x => x.StartTime, opts => opts.MapFrom(x => x.ScheduleType.StartTime));

            CreateMap<ScheduleTypeServiceModel, ScheduleAdminViewModel>()
                .ForMember(x => x.JobCount, opts => opts.MapFrom((src, dst) =>
                {
                    return src.Jobs.Count;
                }
                ))
                .ForMember(x => x.ScheduleName, opts => opts.MapFrom(x => x.Name));

            CreateMap<CountryServiceModel, CountriesAdminViewModel>()
                .ForMember(x => x.CityCount, opts => opts.MapFrom((src, dst) =>
                {
                    return src.Cities.Count;
                }));

            CreateMap<CityServiceModel, CitiesAdminViewModel>()
                .ForMember(x => x.Country, opts => opts.MapFrom(x => x.Country.Name));

            CreateMap<CurrencyServiceModel, CurrenciesAdminViewModel>()
                .ForMember(x => x.SalaryCount, opts => opts.MapFrom((src, dst) =>
                {
                    return src.Salaries.Count;
                }));

            CreateMap<SalaryTypeServiceModel, SalaryTypesAdminViewModel>()
                .ForMember(x => x.SalaryCount, opts => opts.MapFrom((src, dst) =>
                {
                    return src.Salaries.Count;
                }));

            CreateMap<CreateCompanyViewModel, CompanyServiceModel>();

            CreateMap<CreateDepartmentViewModel, DepartmentServiceModel>()
                .ForMember(x => x.Location, opts => opts.MapFrom(x => x.Address));

            CreateMap<CreateCurrencyViewModel, CurrencyServiceModel>();

            CreateMap<CreateSalaryTypeViewModel, SalaryTypeServiceModel>();

            CreateMap<CreateCountryViewModel, CountryServiceModel>();

            CreateMap<CreateCityViewModel, CityServiceModel>();

            CreateMap<CreateScheduleTypeViewModel, ScheduleTypeServiceModel>();

            CreateMap<CreateJobViewModel, JobServiceModel>();
        }
    }
}
