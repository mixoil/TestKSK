using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace TestKSK.Helpers
{
    public class JwtHelper
    {
        private readonly SymmetricSecurityKey _key;

        public JwtHelper(SymmetricSecurityKey securityKey)
        {
            _key = securityKey;
        }

        public string CreateJwtToken(List<(string key, string value)> payload)
        {
            var claims = payload.Select(p => new Claim(p.key, p.value)).ToList();

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool ValidateToken(string authToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = GetValidationParameters();
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out var validatedToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static IEnumerable<Claim> GetJwtTokenClaims(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(token);
                var tokenS = jsonToken as JwtSecurityToken;
                return tokenS.Claims;
            }
            catch
            {
                return null;
            }
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                IssuerSigningKey = _key // The same key as the one that generate the token
            };
        }
    }
}
