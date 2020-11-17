using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Entities
{
    public class Location : IBaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual City City { get; set; } 
        public virtual string PostalCode { get; set; }
    }
}
