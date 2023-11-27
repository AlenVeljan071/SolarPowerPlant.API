using System.Security.Claims;

namespace SolarPowerPlant.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(int userId, string email);
        string SecurityToken(IEnumerable<Claim> claims);
        IEnumerable<Claim> ReadToken(string token);
        int ReadUserId(string token);
        string GenerateRefreshToken();
    }
}
