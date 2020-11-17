using System;
using System.Collections;
using System.Collections.Generic;

namespace ImmedisHCM.Data.Entities
{
    public class Company : IBaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string LegalName { get; set; }
        public virtual IList<Department> Departments { get; set; }
    }
}