using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmedisHCM.Data.Identity.Entities
{
    public class WebUserLogin : IdentityUserLogin<Guid>
    {

        protected bool Equals(WebUserLogin other)
        {
            return LoginProvider == other.LoginProvider
                && ProviderKey == other.ProviderKey;
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
            return Equals((WebUserLogin)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = LoginProvider.GetHashCode();
                hashCode = (hashCode * 397) ^ ProviderKey.GetHashCode();
                return hashCode;
            }
        }

    }
}