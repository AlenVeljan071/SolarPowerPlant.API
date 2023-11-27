using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SolarPowerPlant.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using SolarPowerPlant.Core.Config;

namespace SolarPowerPlant.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<TokenSettings> _options;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IOptions<TokenSettings> options)
        {
            _options = options;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
        }

        public string CreateToken(int userId, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim("UserId", userId.ToString()),
            };

            return SecurityToken(claims);
        }

        public string SecurityToken(IEnumerable<Claim> claims)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = creds,
                Issuer = _options.Value.Issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public IEnumerable<Claim> ReadToken(string token)
        {
            var jwt = "";
            jwt = token["Bearer ".Length..];
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwt);
            return jwtSecurityToken.Claims;
        }

        public int ReadUserId(string token)
        {
            var claims = ReadToken(token);
            return Convert.ToInt32(claims.First(x => x.Type == "UserId").Value);
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
