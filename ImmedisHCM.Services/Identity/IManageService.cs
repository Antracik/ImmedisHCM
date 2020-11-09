using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Identity
{
    public interface IManageService
    {
        Task<IdentityResult> ChangePasswordAsync(UserServiceModel user, string oldPassword, string newPassword);
        Task<UserServiceModel> GetUserAsync(ClaimsPrincipal principal);
        Task<IdentityResult> SetEmailAsync(UserServiceModel user, string email);
        Task<IdentityResult> SetPhoneNumberAsync(UserServiceModel user, string phoneNumber);
        Task SignInAsync(UserServiceModel user, string password, bool isPersistent);
    }
}