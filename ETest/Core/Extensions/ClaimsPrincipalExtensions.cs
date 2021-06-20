using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType).Select(x => x.Value).ToList();
            return result;
        }
        public static string Claim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var value = claimsPrincipal.FindFirst(claimType)?.Value;
            return value ?? "Null";

        }
        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Actor);
        }
        public static int ClaimNameIdentifier(this ClaimsPrincipal claimsPrincipal)
        {

            var result = claimsPrincipal.Claim(ClaimTypes.NameIdentifier);
            if (result=="Null") return 0;
            return Convert.ToInt32(result);
        }
    }
}