using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Identity.Entities
{
    public class WebUserToken : IdentityUserToken<Guid>
    {
        protected bool Equals(WebUserToken other)
        {
            return UserId == other.UserId
                && LoginProvider == other.LoginProvider
                && Name == other.Name;
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
            return Equals((WebUserToken)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = UserId.GetHashCode();
                hashCode = (hashCode * 397) ^ LoginProvider.GetHashCode();
                hashCode = (hashCode * 397) ^ Name.GetHashCode();
                return hashCode;
            }
        }
    }
}
