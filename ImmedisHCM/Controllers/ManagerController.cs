using AutoMapper;
using ImmedisHCM.Services.Core;
using ImmedisHCM.Services.Identity;
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
        private readonly IAccountManageService _accountManageService;
        private readonly IMapper _mapper;

        public ManagerController(IManagerService managerService,
                                 IMapper mapper)
        {
            _managerService = managerService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Employees()
        {
            var employees = await _managerService.GetEmployeesForManager(User.Identity.Name);

            var models = _mapper.Map<List<EmployeesViewModel>>(employees);

            return View(models);
        }
    }
}
