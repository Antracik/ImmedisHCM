using System;
using System.Collections.Generic;

namespace ImmedisHCM.Services.Models.Core
{
    public class DepartmentServiceModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LocationServiceModel Location { get; set; }
        public EmployeeServiceModel Manager { get; set; }
        public CompanyServiceModel Company { get; set; }
        public IList<EmployeeServiceModel> Employees { get; set; }
    }
}