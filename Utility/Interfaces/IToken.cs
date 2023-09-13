using paroquiaRussas.Models;
using System.Security.Claims;

namespace paroquiaRussas.Utility.Interfaces
{
    public interface IToken
    {
        dynamic GenerateToken(IEnumerable<Claim> claims);

        string GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
