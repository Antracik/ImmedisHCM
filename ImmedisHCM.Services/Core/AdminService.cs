using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Data.Infrastructure;
using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public class AdminService : IAdminService
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly UserManager<WebUser> _userManager;


        public AdminService(IUnitOfWork unitOfWork,
                            IMapper mapper,
                            UserManager<WebUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> CreateEmployee(EmployeeServiceModel employee)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var model = _mapper.Map<Employee>(employee);

                var employeeRepo = _unitOfWork.GetRepository<Employee>();
                var locationRepo = _unitOfWork.GetRepository<Location>();
                var salaryRepo = _unitOfWork.GetRepository<Salary>();

                await locationRepo.AddItemAsync(model.Location);
                await salaryRepo.AddItemAsync(model.Salary);
                await employeeRepo.AddItemAsync(model);

                await _unitOfWork.CommitAsync();

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }

        }

        public async Task<List<CompanyServiceModel>> GetCompaniesWithDepartments()
        {
            var companies = await _unitOfWork.GetRepository<Company>().GetAsync(fetch: x => x.FetchMany(x => x.Departments));

            var model = _mapper.Map<List<CompanyServiceModel>>(companies);

            return model;
        }

        public async Task<CompanyServiceModel> GetCompany(Guid id)
        {
            var company = await _unitOfWork.GetRepository<Company>().GetByIdAsync(id);

            var model = _mapper.Map<CompanyServiceModel>(company);

            return model;
        }


        public async Task<List<CompanyServiceModel>> GetCompanies()
        {
            var companies = await _unitOfWork.GetRepository<Company>().GetAsync();

            var model = _mapper.Map<List<CompanyServiceModel>>(companies);

            return model;
        }

        public async Task<DepartmentServiceModel> GetDepartmentById(Guid id)
        {
            var department = await _unitOfWork.GetRepository<Department>().GetSingleAsync(x => x.Id == id);

            var model = _mapper.Map<DepartmentServiceModel>(department);

            return model;
        }

        public async Task<bool> CreateCompany(CompanyServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<Company>(model);

                await _unitOfWork.GetRepository<Company>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> CreateSchedule(ScheduleTypeServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<ScheduleType>(model);

                await _unitOfWork.GetRepository<ScheduleType>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> CreateJob(JobServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<Job>(model);

                await _unitOfWork.GetRepository<Job>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> CreateCountry(CountryServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<Country>(model);

                await _unitOfWork.GetRepository<Country>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> CreateCity(CityServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<City>(model);

                await _unitOfWork.GetRepository<City>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<List<DepartmentServiceModel>> GetDepartmentsByCompanyId(Guid id)
        {
            var departments = await _unitOfWork.GetRepository<Department>().GetAsync(x => x.Company.Id == id);

            var model = _mapper.Map<List<DepartmentServiceModel>>(departments);

            return model;
        }

        public async Task<bool> CreateDepartment(DepartmentServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<Department>(model);
                await _unitOfWork.GetRepository<Location>().AddItemAsync(createModel.Location);
                await _unitOfWork.GetRepository<Department>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> FireEmployee(string employeeEmail)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var empRepo =  _unitOfWork.GetRepository<Employee>();

                var emp = await empRepo.GetSingleAsync(x => x.Email == employeeEmail);

                emp.LeftDate = DateTime.UtcNow;

                await empRepo.UpdateAsync(emp);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }
        public async Task<bool> CreateCurrency(CurrencyServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<Currency>(model);
                await _unitOfWork.GetRepository<Currency>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<bool> CreateSalaryType(SalaryTypeServiceModel model)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var createModel = _mapper.Map<SalaryType>(model);
                await _unitOfWork.GetRepository<SalaryType>().AddItemAsync(createModel);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        }

        public async Task<List<DepartmentServiceModel>> GetDepartments()
        {
            var departments = await _unitOfWork.GetRepository<Department>().GetAsync(fetch: x => x.Fetch(x => x.Employees));

            var model = _mapper.Map<List<DepartmentServiceModel>>(departments);

            return model;
        }

        public async Task<List<JobServiceModel>> GetJobs()
        {
            var companies = await _unitOfWork.GetRepository<Job>().GetAsync();

            var model = _mapper.Map<List<JobServiceModel>>(companies);

            return model;
        }

        public async Task<JobServiceModel> GetJobById(Guid id)
        {
            var department = await _unitOfWork.GetRepository<Job>().GetSingleAsync(x => x.Id == id);

            var model = _mapper.Map<JobServiceModel>(department);

            return model;
        }

        public async Task<IdentityResult> SetUserAsManager(UserServiceModel user)
        {
            var model = _mapper.Map<WebUser>(user);

            return await _userManager.AddToRoleAsync(model, "Manager");
        }

        public async Task<IdentityResult> DeleteUser(UserServiceModel user)
        {
            var model = _mapper.Map<WebUser>(user);

            return await _userManager.DeleteAsync(model);
        }

        public async Task<List<ScheduleTypeServiceModel>> GetJobSchedules()
        {
            var model = await _unitOfWork.GetRepository<ScheduleType>().GetAsync(fetch: x => x.Fetch(x => x.Jobs));

            return _mapper.Map<List<ScheduleTypeServiceModel>>(model);
        }

        public async Task<ScheduleTypeServiceModel> GetJobSchedule(Guid id)
        {
            var model = await _unitOfWork.GetRepository<ScheduleType>().GetSingleAsync(x => x.Id == id);

            return _mapper.Map<ScheduleTypeServiceModel>(model);
        }

        public async Task<List<EmployeeServiceModel>> GetEmployees()
        {
            return _mapper.Map<List<EmployeeServiceModel>>(await _unitOfWork.GetRepository<Employee>().GetAsync());
        }
    }
}
