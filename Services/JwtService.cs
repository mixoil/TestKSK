using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestKSK.Helpers;
using TestKSK.Interfaces;

namespace TestKSK.Services
{
    public class JwtService : IJwtService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly JwtHelper _jwtHelper;

        public JwtService(IConfiguration config)
        {
            var jwtSecret = config["Authorization:JwtSecret"];
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            
            _jwtHelper = new JwtHelper(_key);
        }

        public string CreateToken(List<(string, string)> payload)
        {
            return _jwtHelper.CreateJwtToken(payload);
        }

        public bool ValidateToken(string authToken)
        {
            return _jwtHelper.ValidateToken(authToken);
        }
    }
}
