using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Data.Infrastructure;
using ImmedisHCM.Services.Models.Core;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagerService(IUnitOfWork unitOfWork,
                              IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EmployeeServiceModel>> GetEmployeesForManager(string managerEmail)
        {
            var repo = _unitOfWork.GetRepository<Employee>();
            var employees = await repo.GetAsync(filter: x => x.Manager.Email == managerEmail);

            var model = _mapper.Map<List<EmployeeServiceModel>>(employees);

            return model;
        }

        public async Task<SalaryServiceModel> GetEmployeeSalary(string employeeEmail)
        {
            var salary = await _unitOfWork.GetRepository<Salary>()
                .GetSingleAsync(x => x.Employee.Email == employeeEmail);

            var model = _mapper.Map<SalaryServiceModel>(salary);

            return model;
        }

        public async Task<JobServiceModel> GetEmployeeJob(string employeeEmail)
        {
            var job = (await _unitOfWork.GetRepository<Employee>().GetSingleAsync(x => x.Email == employeeEmail)).Job;

            var model = _mapper.Map<JobServiceModel>(job);

            return model;
        }
        
        public async Task<DepartmentServiceModel> GetDepartmentForEmployeeWithEmployees(string employeeEmail)
        {
            var employee = (await _unitOfWork.GetRepository<Employee>().
                GetSingleAsync(x => x.Email == employeeEmail));

            var department = (await _unitOfWork.GetRepository<Department>()
                .GetAsync(x => x.Id == employee.Department.Id, fetch: x => x.FetchMany(x => x.Employees)))
                .FirstOrDefault();

            var model = _mapper.Map<DepartmentServiceModel>(department);

            return model;
        }

        public async Task<bool> UpdateEmployeeSalary(SalaryServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var updateModel = _mapper.Map<Salary>(model);
                var historyRepo = _unitOfWork.GetRepository<SalaryHistory>();

                var salaryHistory = await PrepareSalaryHistory(model);

                await _unitOfWork.GetRepository<Salary>().UpdateAsync(updateModel);
                await historyRepo.AddItemAsync(salaryHistory);


                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateEmployeeJob(JobServiceModel job, SalaryServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var jobModel = _mapper.Map<Job>(job);
                var updateModel = _mapper.Map<Salary>(model);

                var salaryHistoryRepo = _unitOfWork.GetRepository<SalaryHistory>();
                var employeeRepo = _unitOfWork.GetRepository<Employee>();

                var employee = employeeRepo.GetById(updateModel.Employee.Id);
                employee.Job = jobModel;

                var salaryHistory = await PrepareSalaryHistory(model);

                var jobHistory = new JobHistory
                {
                    DateChanged = salaryHistory.DateChanged,
                    EmployeeId = employee.Id,
                    FromDate = salaryHistory.FromDate,
                    ToDate = salaryHistory.ToDate,
                    Job = jobModel,
                    SalaryHistoryId = salaryHistory.SalaryId
                };

                await _unitOfWork.GetRepository<Salary>().UpdateAsync(updateModel);

                await salaryHistoryRepo.AddItemAsync(salaryHistory);
                await employeeRepo.UpdateAsync(employee);
                await _unitOfWork.GetRepository<JobHistory>().AddItemAsync(jobHistory);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> IsManagerToEmployee(string managerEmail, string employeeEmail)
        {
            var employee = await _unitOfWork.GetRepository<Employee>()
                           .GetSingleAsync(x => x.Email == employeeEmail
                           && x.Manager.Email == managerEmail);

            return employee != null;

        }

        private async Task<SalaryHistory> PrepareSalaryHistory(SalaryServiceModel model)
        {
            var salaryHistoryRepo = _unitOfWork.GetRepository<SalaryHistory>();

            var lastChanged = (await salaryHistoryRepo.GetAsync(x => x.SalaryId == model.Id, x => x.OrderBy(x => x.DateChanged)))
                                .LastOrDefault();
            DateTime fromDate;

            if (lastChanged == null)
                fromDate = model.Employee.HiredDate;
            else
                fromDate = lastChanged.ToDate;

            var salaryHistory = new SalaryHistory
            {
                DateChanged = DateTime.UtcNow,
                Amount = model.Amount,
                FromDate = fromDate,
                ToDate = DateTime.UtcNow,
                Employee = _mapper.Map<Employee>(model.Employee),
                SalaryId = model.Id
            };

            return salaryHistory;
        }


    }
}
