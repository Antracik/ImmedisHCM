using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Services.Identity;
using ImmedisHCM.Web.Models.ManageViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ImmedisHCM.Web.Controllers
{
    [Authorize]
    [Route("Account/[controller]/[action]")]
    public class ManageController : Controller
    {
        private readonly IManageService _manageService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly UrlEncoder _urlEncoder;

        public ManageController(
          IEmailSender emailSender,
          ILogger<ManageController> logger,
          UrlEncoder urlEncoder, IManageService manageService)
        {
            _emailSender = emailSender;
            _logger = logger;
            _urlEncoder = urlEncoder;
            _manageService = manageService;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _manageService.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with email '{User.Identity.Name}'.");
            }
            
            var model = new IndexViewModel
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(IndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _manageService.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with email '{User.Identity.Name}'.");
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _manageService.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _manageService.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Profile));
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _manageService.GetUserAsync(User);
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

            var user = await _manageService.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with email '{User.Identity.Name}'.");
            }

            var changePasswordResult = await _manageService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                AddErrors(changePasswordResult);
                return View(model);
            }

            await _manageService.SignInAsync(user, model.NewPassword, isPersistent: false);
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
