using AutoMapper;
using ImmedisHCM.Services.Core;
using ImmedisHCM.Services.Identity;
using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Services.Models.Identity;
using ImmedisHCM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public readonly INomenclatureService _nomenclatureService;
        public readonly IAccountService _accountService;
        public readonly IManagerService _managerService;
        public readonly IAdminService _adminService;
        public readonly IAccountManageService _accountManageService;
        public readonly IMapper _mapper;

        public AdminController(IMapper mapper,
                               INomenclatureService nomenclatureService,
                               IAdminService adminService,
                               IAccountService accountService,
                               IManagerService managerService, 
                               IAccountManageService accountManageService)
        {
            _mapper = mapper;
            _nomenclatureService = nomenclatureService;
            _adminService = adminService;
            _accountService = accountService;
            _managerService = managerService;
            _accountManageService = accountManageService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            string message = StatusMessage;

            return View(model: message);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            var model = await FillSelectLists();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var selectListModels = await FillSelectLists(model.CountryId, model.CompanyId);

                model = _mapper.Map(selectListModels, model);

                return View(model);
            }

            //VERY SPAGHETTI 

            //Register Identity
            var result = new IdentityResult();

            var user = new UserServiceModel { Email = model.Email, UserName = model.Email };
            result = await _accountService.CreateUserAsync(user, "P@ssw0rd");

            if (model.IsManager && result.Succeeded)
            {
                user = _mapper.Map<UserServiceModel>(await _accountService.GetByIdAsync(user.Id.ToString()));
                result = await _adminService.SetUserAsManager(user);
            }

            //If there was an error with identity, redisplay form
            if (!result.Succeeded)
            {
                var selectListModels = await FillSelectLists(model.CountryId, model.CompanyId);

                model = _mapper.Map(selectListModels, model);

                model.StatusMessage = "Error, could not create employee";

                return View(model);
            }

            //Register Employee 
            var department = await _adminService.GetDepartmentById(model.DepartmentId);
            var job = await _adminService.GetJobById(model.JobId);

            var createModel = new EmployeeServiceModel
            {
                Department = department,
                Job = job,
                HiredDate = DateTime.UtcNow,
                Salary = new SalaryServiceModel
                {
                    Amount = model.SalaryAmount,
                    Currency = await _nomenclatureService.GetCurrency(model.CurrencyId),
                    SalaryType = await _nomenclatureService.GetSalaryType(model.SalaryTypeId)
                },
                HrId = Guid.NewGuid()
            };

            createModel = _mapper.Map(model, createModel);

            createModel.Location.City = await _nomenclatureService.GetCity(model.CityId);

            if (!await _adminService.CreateEmployee(createModel))
            {
                //if employee creation failed, we need to delete the user associated with it
                await _adminService.DeleteUser(user);

                var selectListModels = await FillSelectLists(model.CountryId, model.CompanyId);

                model = _mapper.Map(selectListModels, model);

                model.StatusMessage = "Error, could not create employee";

                return View(model);
            }

            StatusMessage = "Employee Successfuly registered!";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var employees = await _adminService.GetEmployees();

            var model = _mapper.Map<List<EmployeesAdminViewModel>>(employees);

            return View(model);
        }

        [HttpGet]
        [Route("[controller]/Employee/[action]")]
        public async Task<IActionResult> Details(string email)
        {
            var employee = await _accountManageService.GetEmployeeByEmailAsync(email);

            if (employee != null)
            {
                //deal with nulls later
            }

            var model = _mapper.Map<EmployeeDetailsViewModel>(employee);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeSalary(string email)
        {
            var empSalary = await _managerService.GetEmployeeSalary(email);

            var model = _mapper.Map<UpdateEmployeeSalaryViewModel>(empSalary);

            model.Currencies = _mapper.Map<List<CurrencyViewModel>>(await _nomenclatureService.GetCurrencies());
            model.SalaryTypes = _mapper.Map<List<SalaryTypeViewModel>>(await _nomenclatureService.GetSalaryTypes());
            model.StatusMessage = StatusMessage;
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeSalary(UpdateEmployeeSalaryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var salId = (await _managerService.GetEmployeeSalary(model.EmployeeEmail)).Id;

            var updateModel = new SalaryServiceModel
            {
                Amount = model.Amount,
                Currency = await _nomenclatureService.GetCurrency(model.CurrencyId),
                SalaryType = await _nomenclatureService.GetSalaryType(model.SalaryTypeId),
                Employee = await _accountManageService.GetEmployeeByEmailAsync(model.EmployeeEmail),
                Id = salId
            };

            await _managerService.UpdateEmployeeSalary(updateModel);

            StatusMessage = $"Successfuly updated salary for employee with email {model.EmployeeEmail}";

            return RedirectToAction(nameof(ChangeSalary), routeValues: new { email = model.EmployeeEmail });
        }

        [HttpGet]
        public async Task<IActionResult> ChangeJob(string email)
        {
            var job = await _managerService.GetEmployeeJob(email);
            var salary = await _managerService.GetEmployeeSalary(email);

            var model = new UpdateEmployeeJobViewModel
            {
                Amount = salary.Amount,
                Currencies = _mapper.Map<List<CurrencyViewModel>>(await _nomenclatureService.GetCurrencies()),
                SalaryTypes = _mapper.Map<List<SalaryTypeViewModel>>(await _nomenclatureService.GetSalaryTypes()),
                CurrencyId = salary.Currency.Id,
                EmployeeEmail = email,
                EmployeeName = $"{salary.Employee.FirstName} {salary.Employee.LastName}",
                SalaryTypeId = salary.SalaryType.Id,
                Jobs = _mapper.Map<List<JobViewModel>>(await _adminService.GetJobs()),
                JobId = job.Id,
                StatusMessage = StatusMessage
                
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeJob(UpdateEmployeeJobViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var salId = (await _managerService.GetEmployeeSalary(model.EmployeeEmail)).Id;
            var salaryUpdateModel = new SalaryServiceModel
            {
                Amount = model.Amount,
                Currency = await _nomenclatureService.GetCurrency(model.CurrencyId),
                SalaryType = await _nomenclatureService.GetSalaryType(model.SalaryTypeId),
                Employee = await _accountManageService.GetEmployeeByEmailAsync(model.EmployeeEmail),
                Id = salId
            };

            var jobModel = await _adminService.GetJobById(model.JobId);

            await _managerService.UpdateEmployeeJob(jobModel, salaryUpdateModel);

            StatusMessage = $"Successfuly updated job for employee with email {model.EmployeeEmail}";

            return RedirectToAction(nameof(ChangeJob), routeValues: new { email = model.EmployeeEmail });
        }

        [HttpGet]
        public async Task<IActionResult> Companies()
        {
            var companies = await _adminService.GetCompaniesWithDepartments();

            var model = _mapper.Map<List<CompanyAdminViewModel>>(companies);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CreateCompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<CompanyServiceModel>(model);

            await _adminService.CreateCompany(createModel);

            return RedirectToAction(nameof(Companies));
        }

        [HttpGet]
        public async Task<IActionResult> CreateDepartment()
        {
            var countries = _mapper.Map<List<CountryViewModel>>(await _nomenclatureService.GetCountries());
            var companies = _mapper.Map<List<CompanyViewModel>>(await _adminService.GetCompanies());

            var model = new CreateDepartmentViewModel
            {
                Cities = _mapper.Map<List<CityViewModel>>(await _nomenclatureService.GetCitiesByCountryId(countries[0].Id)),
                Countries = countries,
                Companies = companies
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<DepartmentServiceModel>(model);

            createModel.Location.City = await _nomenclatureService.GetCity(model.CityId);
            createModel.Company = await _adminService.GetCompany(model.CompanyId);

            await _adminService.CreateDepartment(createModel);

            return RedirectToAction(nameof(Departments));
        }



        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            var departments = await _adminService.GetDepartments();

            var model = _mapper.Map<List<DepartmentsAdminViewModel>>(departments);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Jobs()
        {
            var jobs = await _adminService.GetJobs();

            var model = _mapper.Map<List<JobAdminViewModel>>(jobs);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateJob()
        {
            var schedules = await _adminService.GetJobSchedules();

            var model = new CreateJobViewModel
            {
                Schedules = _mapper.Map<List<ScheduleTypeViewModel>>(schedules)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<JobServiceModel>(model);

            createModel.ScheduleType = await _adminService.GetJobSchedule(model.ScheduleId);

            await _adminService.CreateJob(createModel);

            return RedirectToAction(nameof(Jobs));
        }

        [HttpGet]
        public async Task<IActionResult> Schedules()
        {
            var schedules = await _adminService.GetJobSchedules();

            var model = _mapper.Map<List<ScheduleAdminViewModel>>(schedules);

            return View(model);
        }
        
        [HttpGet]
        public IActionResult CreateSchedule()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateScheduleTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<ScheduleTypeServiceModel>(model);

            await _adminService.CreateSchedule(createModel);

            return RedirectToAction(nameof(Schedules));
        }

        [HttpGet]
        public async Task<IActionResult> Countries()
        {
            var countries = await _nomenclatureService.GetCountriesWithCities();

            var model = _mapper.Map<List<CountriesAdminViewModel>>(countries);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateCountry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(CreateCountryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<CountryServiceModel>(model);

            await _adminService.CreateCountry(createModel);

            return RedirectToAction(nameof(Countries));
        }

        [HttpGet]
        public async Task<IActionResult> Cities()
        {
            var cities = await _nomenclatureService.GetCities();

            var model = _mapper.Map<List<CitiesAdminViewModel>>(cities);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCity()
        {
            var model = new CreateCityViewModel
            {
                Countries = _mapper.Map<List<CountryViewModel>>(await _nomenclatureService.GetCountries())
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(CreateCityViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<CityServiceModel>(model);

            createModel.Country = await _nomenclatureService.GetCountry(model.CountryId);

            await _adminService.CreateCity(createModel);

            return RedirectToAction(nameof(Cities));
        }

        [HttpGet]
        public async Task<IActionResult> Currencies()
        {
            var currencies = await _nomenclatureService.GetCurrenciesWithSalaries();

            var model = _mapper.Map<List<CurrenciesAdminViewModel>>(currencies);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateCurrency()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency(CreateCurrencyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<CurrencyServiceModel>(model);

            await _adminService.CreateCurrency(createModel);

            return RedirectToAction(nameof(Currencies));
        }

        [HttpGet]
        public async Task<IActionResult> SalaryTypes()
        {
            var salaryTypes = await _nomenclatureService.GetSalaryTypesWithSalaries();

            var model = _mapper.Map<List<SalaryTypesAdminViewModel>>(salaryTypes);

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateSalaryType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSalaryType(CreateSalaryTypeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var createModel = _mapper.Map<SalaryTypeServiceModel>(model);

            await _adminService.CreateSalaryType(createModel);

            return RedirectToAction(nameof(SalaryTypes));

        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(string email)
        {
            await _adminService.FireEmployee(email);

            return RedirectToAction(nameof(Employees));

        }


        private async Task<CreateEmployeeViewModel> FillSelectLists(Guid countryId = default, Guid companyId = default)
        {
            var countries = _mapper.Map<List<CountryViewModel>>(await _nomenclatureService.GetCountries());

            if (countryId == default)
                countryId = countries[0].Id;

            var cities = _mapper.Map<List<CityViewModel>>(await _nomenclatureService.GetCitiesByCountryId(countryId));
            var currencies = _mapper.Map<List<CurrencyViewModel>>(await _nomenclatureService.GetCurrencies());
            var salaryTypes = _mapper.Map<List<SalaryTypeViewModel>>(await _nomenclatureService.GetSalaryTypes());

            var companies = _mapper.Map<List<CompanyViewModel>>(await _adminService.GetCompaniesWithDepartments());

            if (companyId == default)
                companyId = companies[0].Id;

            var departments = _mapper.Map<List<DepartmentViewModel>>(await _adminService.GetDepartmentsByCompanyId(companyId));

            var jobs = _mapper.Map<List<JobViewModel>>(await _adminService.GetJobs());

            var model = new CreateEmployeeViewModel
            {
                Jobs = jobs,
                SalaryTypes = salaryTypes,
                Currencies = currencies,
                Countries = countries,
                Cities = cities,
                Companies = companies,
                Departments = departments
            };

            return model;
        }
    }
}
