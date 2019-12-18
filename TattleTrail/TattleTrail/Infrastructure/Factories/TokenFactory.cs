using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TattleTrail.Infrastructure.Factories {
    public class TokenFactory : ITokenFactory {
        private readonly IConfiguration _configuration;
        public TokenFactory(IConfiguration configuration) {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void CreateToken() {

            var now = DateTime.UtcNow;
            var signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["SIGNING_KEY"]));

            var jwt = new JwtSecurityToken(
                    issuer: _configuration["VALID_ISSUER"],
                    audience: _configuration["VALID_AUDIENCE"],
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(1)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var token = new {
                access_token = encodedJwt
            };
        }
    }
}
