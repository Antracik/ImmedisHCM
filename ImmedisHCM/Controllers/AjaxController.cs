using AutoMapper;
using ImmedisHCM.Services.Core;
using ImmedisHCM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmedisHCM.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class AjaxController : Controller
    {
        private readonly INomenclatureService _nomenclatureService;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AjaxController(INomenclatureService nomenclatureService,
                              IMapper mapper, 
                              IAdminService adminService)
        {
            _nomenclatureService = nomenclatureService;
            _mapper = mapper;
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCitiesForCountry(string Id)
        {
            var cities = await _nomenclatureService.GetCitiesByCountryId(new Guid(Id));
            
            var model = _mapper.Map<List<CityViewModel>>(cities);

            return new JsonResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentsForCompany(string Id)
        {
            var cities = await _adminService.GetDepartmentsByCompanyId(new Guid(Id));

            var model = _mapper.Map<List<DepartmentViewModel>>(cities);

            return new JsonResult(model);
        }
    }
}
