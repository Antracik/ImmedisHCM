﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities
{
    public class Department : IBaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Location Location { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual Company Company { get; set; }
        public virtual IList<Employee> Employees { get; set; }

    }
}
