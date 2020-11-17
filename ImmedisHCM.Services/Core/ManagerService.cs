using AutoMapper;
using ImmedisHCM.Data.Entities;
using ImmedisHCM.Data.Infrastructure;
using ImmedisHCM.Services.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ImmedisHCM.Services.Core
{
    public class ManagerService : IManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<EmployeeServiceModel>> GetEmployeesForManager(string managerEmail)
        {

            var repo = _unitOfWork.GetRepository<Employee>();
            var employees = await repo.GetAsync(filter: x => x.Manager.Email == managerEmail);

            var model = _mapper.Map<List<EmployeeServiceModel>>(employees);

            return model;
        }
    }
}
