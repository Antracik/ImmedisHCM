using AutoMapper;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Identity
{
    public class ManageService : IManageService
    {
        private readonly UserManager<WebUser> _userManager;
        private readonly SignInManager<WebUser> _signInManager;
        private readonly IMapper _mapper;

        public ManageService(SignInManager<WebUser> signInManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _mapper = mapper;
        }

        public async Task<UserServiceModel> GetUserAsync(ClaimsPrincipal principal)
        {
            var model = await _userManager.GetUserAsync(principal);
            if (model == null)
                return null;

            var user = _mapper.Map<UserServiceModel>(model);
            return user;
        }

        public async Task<IdentityResult> SetEmailAsync(UserServiceModel user, string email)
        {
            var model = _mapper.Map<WebUser>(user);
            return await _userManager.SetEmailAsync(model, email);
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(UserServiceModel user, string phoneNumber)
        {
            var model = _mapper.Map<WebUser>(user);
            return await _userManager.SetPhoneNumberAsync(model, phoneNumber);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserServiceModel user, string oldPassword, string newPassword)
        {
            var model = _mapper.Map<WebUser>(user);

            return await _userManager.ChangePasswordAsync(model, oldPassword, newPassword);
        }

        public async Task SignInAsync(UserServiceModel user, string password, bool isPersistent)
        {
            var model = _mapper.Map<WebUser>(user);

            await _signInManager.PasswordSignInAsync(model, password, isPersistent, false);
        }
    }
}
