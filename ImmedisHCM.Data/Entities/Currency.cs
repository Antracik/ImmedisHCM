using System.Collections.Generic;

namespace ImmedisHCM.Data.Entities
{
    public class Currency
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Salary> Salaries { get; set; }
    }
}