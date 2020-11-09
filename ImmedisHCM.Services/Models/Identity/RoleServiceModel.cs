using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Services.Models.Identity
{
    public class RoleServiceModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NormalizedName { get; set; }
    }
}
