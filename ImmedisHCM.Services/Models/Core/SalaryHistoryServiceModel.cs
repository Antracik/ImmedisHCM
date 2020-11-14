using System;

namespace ImmedisHCM.Services.Models.Core
{
    public class SalaryHistoryServiceModel
    {
        public DateTime DateChanged { get; set; }
        public Guid SalaryId { get; set; }
        public SalaryServiceModel Salary { get; set; }
        public EmployeeServiceModel Employee { get; set; }
        public decimal Amount { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
