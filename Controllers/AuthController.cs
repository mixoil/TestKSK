using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TestKSK.Interfaces;

namespace TestKSK.Controllers
{
    public class AuthController : Controller
    {
        private readonly IJwtService jwtService;

        public AuthController(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        [HttpGet("/get-token")]
        public async Task<string> GetAuthToken()
        {
            //TODO
            var claims = new List<(string, string)> {
                (JwtRegisteredClaimNames.NameId, "TestUsername"),
            };

            return jwtService.CreateToken(claims);
        }
    }
}
