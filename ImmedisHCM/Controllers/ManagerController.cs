using AutoMapper;
using ImmedisHCM.Services.Core;
using ImmedisHCM.Services.Identity;
using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Controllers
{
    [Authorize(Roles = "Manager")]
    [Route("[controller]/[action]")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;
        private readonly INomenclatureService _nomenclatureService;
        private readonly IAdminService _adminService;
        private readonly IAccountManageService _accountManageService;
        private readonly IMapper _mapper;

        public ManagerController(IManagerService managerService,
                                 IMapper mapper,
                                 IAccountManageService accountManageService,
                                 INomenclatureService nomenclatureService,
                                 IAdminService adminService)
        {
            _managerService = managerService;
            _mapper = mapper;
            _accountManageService = accountManageService;
            _nomenclatureService = nomenclatureService;
            _adminService = adminService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            var employees = await _managerService.GetEmployeesForManager(User.Identity.Name);

            var models = _mapper.Map<List<EmployeesViewModel>>(employees);

            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Department()
        {
            var department = await _managerService.GetDepartmentForEmployeeWithEmployees(User.Identity.Name);

            var models = _mapper.Map<DepartmentDetailsViewModel>(department);

            return View(models);
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
            if (!(await _managerService.IsManagerToEmployee(User.Identity.Name, email)))
                return RedirectToAction(nameof(AccountController.AccessDenied), "Account");

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
            if (!(await _managerService.IsManagerToEmployee(User.Identity.Name, email)))
                return RedirectToAction(nameof(AccountController.AccessDenied), "Account");

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


    }
}
