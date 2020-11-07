using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Identity.Entities
{
    public class WebUserRole : IdentityUserRole<Guid>
    {

        protected bool Equals(WebUserRole other)
        {
            return RoleId == other.RoleId
                && UserId == other.UserId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((WebUserRole)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = RoleId.GetHashCode();
                hashCode = (hashCode * 397) ^ UserId.GetHashCode();
                return hashCode;
            }
        }
    }
}
