using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Client.RCL.Helpers
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];

            var jsonBytes = ParseBase64WithoutPadding(payload);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            var values = keyValuePairs.Select(e => e.Value.ToString()).ToList();

            ExtractClaimTypesFromJWT(ClaimTypes.Role, claims, keyValuePairs);
            ExtractClaimTypesFromJWT("TenantId", claims, keyValuePairs);
            ExtractClaimTypesFromJWT("TenantHost", claims, keyValuePairs);

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private static void ExtractClaimTypesFromJWT(string claimType, List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue(claimType, out object items);
            if (items != null)
            {
                var parsedRoles = items.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
                if (parsedRoles.Length > 1)
                {
                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(claimType, parsedRole.Trim('"')));
                    }
                }
                else
                {
                    claims.Add(new Claim(claimType, parsedRoles[0]));
                }
                keyValuePairs.Remove(claimType);
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
