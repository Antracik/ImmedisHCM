using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Data.Identity.Entities;
using ImmedisHCM.Data.Infrastructure;
using ImmedisHCM.Services.Models.Core;
using ImmedisHCM.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using NHibernate.Linq;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Identity
{
    public class AccountManageService : IAccountManageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<WebUser> _userManager;
        private readonly IMapper _mapper;

        public AccountManageService(IMapper mapper,
                             IUnitOfWork unitOfWork,
                             UserManager<WebUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IdentityResult> SetUserEmailAsync(UserServiceModel user, string email)
        {
            var model = _mapper.Map<WebUser>(user);
            return await _userManager.SetEmailAsync(model, email);
        }

        public async Task<IdentityResult> SetUserPhoneNumberAsync(UserServiceModel user, string phoneNumber)
        {
            var model = _mapper.Map<WebUser>(user);
            return await _userManager.SetPhoneNumberAsync(model, phoneNumber);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserServiceModel user, string oldPassword, string newPassword)
        {
            var model = _mapper.Map<WebUser>(user);

            return await _userManager.ChangePasswordAsync(model, oldPassword, newPassword);
        }

        public async Task<EmployeeServiceModel> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _unitOfWork.GetRepository<Employee>()
                .GetSingleAsync(filter: x => x.Email == email,
                          fetch: x => x.Fetch(x => x.Manager)
                          .Fetch(x => x.Location).ThenFetch(x => x.City).ThenFetch(x => x.Country)
                          .Fetch(x => x.Department).ThenFetch(x => x.Location).ThenFetch(x => x.City).ThenFetch(x => x.Country)
                          .Fetch(x => x.Department).ThenFetch(x => x.Manager)
                          .Fetch(x => x.Salary).ThenFetch(x => x.Currency)
                          .Fetch(x => x.EmergencyContact).ThenFetch(x => x.Location).ThenFetch(x => x.City).ThenFetch(x => x.Country)
                          .Fetch(x => x.Job).ThenFetch(x => x.ScheduleType));

            if (employee == null)
                return null;

            return _mapper.Map<Employee, EmployeeServiceModel>(employee);
        }

        public async Task<EmergencyContactServiceModel> GetEmergencyContactAsync(string employeeEmil)
        {
            var emergencyContact = await _unitOfWork.GetRepository<EmergencyContact>()
                .GetSingleAsync(x => x.Employee.Email == employeeEmil);

            if (emergencyContact == null)
                return null;

            return _mapper.Map<EmergencyContactServiceModel>(emergencyContact);
        }

        public async Task<bool> UpdateEmployee(EmployeeServiceModel employee)
        {
            var model = _mapper.Map<Employee>(employee);

            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.GetRepository<Employee>().UpdateAsync(model);
                
                var locationRepo = _unitOfWork.GetRepository<Location>();

                var employeeLocation = locationRepo.GetById(model.Location.Id);
                employeeLocation = _mapper.Map(model.Location, employeeLocation);

                await locationRepo.UpdateAsync(employeeLocation);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }

        }

        public async Task<bool> UpdateEmergencyContact(EmergencyContactServiceModel emergencyContact)
        {
            var model = _mapper.Map<EmergencyContact>(emergencyContact);
            try
            {
                _unitOfWork.BeginTransaction();

                var emergencyRepo = _unitOfWork.GetRepository<EmergencyContact>();
                var locationRepository = _unitOfWork.GetRepository<Location>();
                await emergencyRepo.UpdateAsync(model);
                await locationRepository.UpdateAsync(model.Location);

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
    }
}
