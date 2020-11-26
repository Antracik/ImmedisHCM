using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public interface IAdminService
    {
        Task<bool> CreateEmployee(EmployeeServiceModel employee);
        Task<List<CompanyServiceModel>> GetCompaniesWithDepartments();
        Task<List<CompanyServiceModel>> GetCompanies();
        Task<CompanyServiceModel> GetCompany(Guid id);
        Task<bool> CreateCompany(CompanyServiceModel model);
        Task<List<JobServiceModel>> GetJobs();
        Task<List<DepartmentServiceModel>> GetDepartmentsByCompanyId(Guid id);
        Task<bool> CreateCountry(CountryServiceModel model);
        Task<bool> CreateDepartment(DepartmentServiceModel model);
        Task<bool> CreateCurrency(CurrencyServiceModel model);
        Task<bool> CreateSchedule(ScheduleTypeServiceModel model);
        Task<DepartmentServiceModel> GetDepartmentById(Guid id);
        Task<bool> CreateCity(CityServiceModel model);
        Task<bool> CreateJob(JobServiceModel model);
        Task<List<DepartmentServiceModel>> GetDepartments();
        Task<bool> CreateSalaryType(SalaryTypeServiceModel model);
        Task<JobServiceModel> GetJobById(Guid id);
        Task<IdentityResult> SetUserAsManager(UserServiceModel user);
        Task<IdentityResult> DeleteUser(UserServiceModel user);
        Task<List<EmployeeServiceModel>> GetEmployees();
        Task<List<ScheduleTypeServiceModel>> GetJobSchedules();
        Task<ScheduleTypeServiceModel> GetJobSchedule(Guid id);
        Task<bool> FireEmployee(string employeeEmail);
    }
}