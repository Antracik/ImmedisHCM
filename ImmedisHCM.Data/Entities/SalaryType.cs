using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities
{
    public class SalaryType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Salary> Salaries { get; set; }
    }
}
