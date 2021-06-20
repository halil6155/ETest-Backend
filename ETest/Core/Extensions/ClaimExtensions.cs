using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
namespace Core.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddUsername(this ICollection<Claim> claims, string username)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, username));
        }

        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, name));
        }
        public static void AddImageUrl(this ICollection<Claim> claims, string imageUrl)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Typ, imageUrl));
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, nameIdentifier));
        }

        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(JwtRegisteredClaimNames.Actort, role)));
        }
    }
}