using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using ImmedisHCM.Services.Core;
using ImmedisHCM.Services.Identity;
using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImmedisHCM.Web.Controllers
{
    [Authorize]
    [Route("Account/Manage/[action]")]
    public class AccountManageController : Controller
    {
        private readonly IAccountManageService _accountManageService;
        private readonly IAccountService _accountService;
        private readonly INomenclatureService _nomenclatureService;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public AccountManageController(IEmailSender emailSender,
                                ILogger<AccountManageController> logger,
                                IAccountManageService manageService,
                                IAccountService accountService,
                                IMapper mapper,
                                INomenclatureService nomenclatureService)
        {
            _emailSender = emailSender;
            _logger = logger;
            _accountManageService = manageService;
            _accountService = accountService;
            _mapper = mapper;
            _nomenclatureService = nomenclatureService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _accountService.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with email '{User.Identity.Name}'.");
            }

            var employee = await _accountManageService.GetEmployeeByEmailAsync(user.Email);

            var model = _mapper.Map<ProfileViewModel>(user);

            if (employee != null)
                model = _mapper.Map(employee, model);

            model.StatusMessage = StatusMessage;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Username = model.Email;
                var location = await _accountManageService.GetEmployeeLocation(model.Email);
                model.City = _mapper.Map<CityViewModel>(location.City);
                model.Country = _mapper.Map<CountryViewModel>(location.City.Country);
                return View(model);
            }

            IdentityResult result = new IdentityResult();
            var user = await _accountService.GetUserAsync(User);
            if (user == null)
                throw new ApplicationException($"Unable to load user with email '{User.Identity.Name}'.");

            var email = user.Email;
            if (model.Email != email)
            {
                result = await _accountManageService.SetUserEmailAsync(user, model.Email);

                if (!result.Succeeded)
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                result = await _accountManageService.SetUserPhoneNumberAsync(user, model.PhoneNumber);
                if (!result.Succeeded)
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
            }

            if (!User.IsInRole("Admin"))
            {
                var updateModel = await _accountManageService.GetEmployeeByEmailAsync(User.Identity.Name);
                updateModel = _mapper.Map(model, updateModel);
                await _accountManageService.UpdateEmployee(updateModel);
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        public async Task<IActionResult> EmergencyContact()
        {
            var contact = await _accountManageService.GetEmergencyContactAsync(User.Identity.Name);

            var countries = _mapper.Map<List<CountryViewModel>>(await _nomenclatureService.GetCountries());
            var cities = _mapper.Map<List<CityViewModel>>(await _nomenclatureService.GetCitiesByCountryId(countries[0].Id));

            if (contact == null)
            {
                contact = new EmergencyContactServiceModel();
            }

            var model = _mapper.Map<EmergencyContactViewModel>(contact);
            model.Countries = countries;

            if (contact.Location == null)
            {
                model.Cities = cities;
            }
            else
            {
                model.Cities = _mapper.Map<List<CityViewModel>>(await _nomenclatureService.GetCitiesByCountryId(contact.Location.City.Country.Id));
                model.CityId = contact.Location.City.Id;
                model.CountryId = contact.Location.City.Country.Id;
            }
            model.StatusMessage = StatusMessage;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EmergencyContact(EmergencyContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Countries = _mapper.Map<List<CountryViewModel>>(await _nomenclatureService.GetCountries());
                model.Cities = _mapper.Map<List<CityViewModel>>(await _nomenclatureService.GetCitiesByCountryId(model.CountryId));
                return View(model);
            }

            var contact = await _accountManageService.GetEmergencyContactAsync(User.Identity.Name);

            var city = await _nomenclatureService.GetCity(model.CityId);

            var serviceModel = _mapper.Map(model, contact);

            serviceModel.Location.City = city;

            if (contact != null)
            {
                await _accountManageService.UpdateEmergencyContact(serviceModel);
            }
            else
            {
                serviceModel.Employee = await _accountManageService.GetEmployeeByEmailAsync(User.Identity.Name);
                await _accountManageService.CreateEmergencyContact(serviceModel);
            }

            StatusMessage = "Your emergency contact has been updated!";

            return RedirectToAction(nameof(EmergencyContact));
        }


        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _accountService.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with email '{User.Identity.Name}'.");
            }

            var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _accountService.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with email '{User.Identity.Name}'.");
            }

            var changePasswordResult = await _accountManageService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _accountService.PasswordSignInAsync(user, model.NewPassword, isPersistent: false);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToAction(nameof(ChangePassword));
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
}
