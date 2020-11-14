using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class SalaryServiceModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public EmployeeServiceModel Employee { get; set; }
        public SalaryTypeServiceModel SalaryType { get; set; }
        public CurrencyServiceModel Currency { get; set; }
    }
}