using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities
{
    public class Country
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ShortName { get; set; }
        public virtual IList<City> Cities { get; set; }
    }
}
