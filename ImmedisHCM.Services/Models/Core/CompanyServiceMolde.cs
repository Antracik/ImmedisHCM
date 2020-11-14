using System;
using System.Collections.Generic;

namespace ImmedisHCM.Services.Models.Core
{
    public class CompanyServiceMolde
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public IList<DepartmentServiceModel> Departments { get; set; }
    }
}