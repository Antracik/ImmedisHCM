using System.Collections.Generic;

namespace ImmedisHCM.Services.Models.Core
{
    public class CurrencyServiceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<SalaryServiceModel> Salaries { get; set; }
    }
}