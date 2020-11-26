using ImmedisHCM.Services.Models.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public interface IManagerService
    {
        Task<List<EmployeeServiceModel>> GetEmployeesForManager(string managerEmail);
        Task<SalaryServiceModel> GetEmployeeSalary(string employeeEmail);
        Task<JobServiceModel> GetEmployeeJob(string employeeEmail);
        Task<DepartmentServiceModel> GetDepartmentForEmployeeWithEmployees(string employeeEmail);
        Task<bool> UpdateEmployeeSalary(SalaryServiceModel model);
        Task<bool> UpdateEmployeeJob(JobServiceModel job, SalaryServiceModel model);
        Task<bool> IsManagerToEmployee(string managerEmail, string employeeEmail);
    }
}