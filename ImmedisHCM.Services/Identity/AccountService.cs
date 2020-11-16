using AutoMapper;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Data.Infrastructure;
using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Identity
{
    public class AccountService : IAccountService
    {
        public readonly UserManager<WebUser> _userManager;
        public readonly SignInManager<WebUser> _signInManager;
        public readonly IMapper _mapper;
        public readonly IUnitOfWork _unitOfWork;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork, SignInManager<WebUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<IdentityResult> CreateUserAsync(UserServiceModel user, string password)
        {
            var model = _mapper.Map<WebUser>(user);
            IdentityResult res = await _userManager.CreateAsync(model, password);
            user.Id = model.Id;
            return res;
        }

        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent)
        {
            return _signInManager.PasswordSignInAsync(userName, password, isPersistent, false);
        }

        public async Task PasswordSignInAsync(UserServiceModel user, string password, bool isPersistent)
        {
            var model = _mapper.Map<WebUser>(user);

            await _signInManager.PasswordSignInAsync(model, password, isPersistent, false);
        }

        public async Task<UserServiceModel> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;
            var model = _mapper.Map<UserServiceModel>(user);
            return model;
        }

        public async Task<UserServiceModel> GetByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;
            var model = _mapper.Map<UserServiceModel>(user);
            return model;
        }

        public async Task<UserServiceModel> GetUserAsync(ClaimsPrincipal principal)
        {
            var model = await _userManager.GetUserAsync(principal);
            if (model == null)
                return null;

            var user = _mapper.Map<UserServiceModel>(model);
            return user;
        }

        public string GetUserId(ClaimsPrincipal principal)
        {
            return _userManager.GetUserId(principal);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }

       
    }
}
