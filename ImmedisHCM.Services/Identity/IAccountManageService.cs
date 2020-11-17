using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Identity
{
    public interface IAccountManageService
    {
        Task<IdentityResult> ChangePasswordAsync(UserServiceModel user, string oldPassword, string newPassword);
        Task<IdentityResult> SetUserEmailAsync(UserServiceModel user, string email);
        Task<IdentityResult> SetUserPhoneNumberAsync(UserServiceModel user, string phoneNumber);
        Task<bool> UpdateEmployee(EmployeeServiceModel employee);
        Task<bool> UpdateEmergencyContact(EmergencyContactServiceModel emergencyContact);
        Task<EmployeeServiceModel> GetEmployeeByEmailAsync(string email);
        Task<EmergencyContactServiceModel> GetEmergencyContactAsync(string employeeEmil);
    }
}