using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using TestKSK.Helpers;
using TestKSK.Interfaces;

namespace TestKSK.Auth
{
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationOptions>
    {
        private readonly IJwtService jwtService;

        public CustomAuthenticationHandler(
            IOptionsMonitor<CustomAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IJwtService jwtService)
            : base(options, logger, encoder, clock)
        {
            this.jwtService = jwtService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var tokenAdded = Request.Query.TryGetValue("secretToken", out var token);
            var strToken = token.ToString();
            if (!tokenAdded)
                return AuthenticateResult.Fail("Token wasn't provided in \"secretToken\" query parameter!");
            var tokenIsValid = jwtService.ValidateToken(strToken); 
            if (!tokenIsValid)
                return AuthenticateResult.Fail("Provided token failed validation!");

            var claims = JwtHelper.GetJwtTokenClaims(strToken);

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
