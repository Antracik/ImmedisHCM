using System.Collections.Generic;

namespace ImmedisHCM.Services.Models.Core
{
    public class SalaryTypeServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<SalaryServiceModel> Salaries { get; set; }
    }
}