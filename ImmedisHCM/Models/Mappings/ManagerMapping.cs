using AutoMapper;
using ImmedisHCM.Services.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Models.Mappings
{
    public class ManagerMapping : Profile
    {
        public ManagerMapping()
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


            CreateMap<EmployeeServiceModel, EmployeeDetailsViewModel>()
                .ForMember(x => x.EmployeeName, opts => opts.MapFrom((src, dest) =>
                {
                    return $"{src.FirstName} {src.LastName}";
                }
                ))
                .ForMember(x => x.EmergencyName, opts => opts.PreCondition(x => x.Manager != null))
                .ForMember(x => x.EmergencyName, opts => opts.MapFrom((src, dest) =>
                {
                    return $"{src.EmergencyContact.FirstName} {src.EmergencyContact.LastName}";
                }
                ))
                .ForMember(x => x.Amount, opts => opts.MapFrom((src, dest) =>
                {
                    return $"{src.Salary.Amount} {src.Salary.Currency.Name}";
                }
                ))
                .ForMember(x => x.CityCountry, opts => opts.MapFrom((src, dest) =>
                {
                    return $"{src.Location.City.Country.Name} ({src.Location.City.Country.ShortName}) / {src.Location.City.Name}";
                }))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(x => x.Department.Name))
                .ForMember(x => x.CompanyName, opts => opts.MapFrom(x => x.Department.Company.Name))
                .ForMember(x => x.Address, opts => opts.MapFrom(x => x.Location))
                .ForMember(x => x.EmergencyEmail, opts => opts.MapFrom(x => x.EmergencyContact.Email))
                .ForMember(x => x.EmergencyAddress, opts => opts.MapFrom(x => x.EmergencyContact.Location))
                .ForMember(x => x.EmergencyPhoneNumber, opts => opts.MapFrom(x => x.EmergencyContact.PhoneNumber))
                .ForMember(x => x.EmergencyHomePhoneNumber, opts => opts.MapFrom(x => x.EmergencyContact.HomePhoneNumber))
                .ForMember(x => x.Amount, opts => opts.MapFrom(x => x.Salary.Amount))
                .ForMember(x => x.SalaryTypeName, opts => opts.MapFrom(x => x.Salary.SalaryType.Name))
                .ForMember(x => x.JobName, opts => opts.MapFrom(x => x.Job.Name))
                .ForMember(x => x.ScheduleName, opts => opts.MapFrom(x => x.Job.ScheduleType.Name))
                .ForMember(x => x.ScheduleHours, opts => opts.MapFrom(x => x.Job.ScheduleType.Hours))
                .ForMember(x => x.ScheduleStartTime, opts => opts.MapFrom(x => x.Job.ScheduleType.StartTime));


            CreateMap<DepartmentServiceModel, DepartmentDetailsViewModel>()
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.Employees, opts => opts.MapFrom(x => x.Employees))
                .ForMember(x => x.Address, opts => opts.MapFrom(x => x.Location))
                .ForMember(x => x.City, opts => opts.MapFrom(x => x.Location.City.Name))
                .ForMember(x => x.Country, opts => opts.MapFrom((src, dst) =>
                {
                    return $"{src.Location.City.Country.Name} ({src.Location.City.Country.ShortName})";
                }))
                .ForMember(x => x.Company, opts => opts.MapFrom(x => x.Company.Name))
                .ForMember(x => x.ManagerEmail, opts => opts.MapFrom(x => x.Manager.Email))
                .ForMember(x => x.ManagerName, opts => opts.MapFrom((src, dst )=>
                {
                    return $"{src.Manager.FirstName} {src.Manager.LastName}";
                }
                ));

        }
    }
}
