using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Identity
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUserAsync(UserServiceModel user, string password);
        Task<UserServiceModel> GetByEmailAsync(string email);
        Task<UserServiceModel> GetByIdAsync(string id);
        Task<UserServiceModel> GetUserAsync(ClaimsPrincipal principal);
        string GetUserId(ClaimsPrincipal principal);
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent);
        Task PasswordSignInAsync(UserServiceModel user, string password, bool isPersistent);
        Task SignOutAsync();
        
    }
}