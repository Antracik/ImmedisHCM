using ImmedisHCM.Services.Models.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public interface IManagerService
    {
        Task<List<EmployeeServiceModel>> GetEmployeesForManager(string managerEmail);
    }
}