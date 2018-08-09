using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace EventTracker.Services
{
    public class ClaimsFactory
    {
        public static ClaimsIdentity GetClaimsIdentity(IEnumerable<Claim> claims, string authenticationType)
        {
            return new ClaimsIdentity(claims, authenticationType);
        }

        public static ClaimsPrincipal GetClaimsPrincipal(IIdentity identity)
        {
            return new ClaimsPrincipal(identity);
        }

    }
}
