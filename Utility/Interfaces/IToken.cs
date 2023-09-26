using Microsoft.AspNetCore.Mvc.Filters;
using paroquiaRussas.Models;
using System.Security.Claims;

namespace paroquiaRussas.Utility.Interfaces
{
    public interface IToken
    {
        void GenerateToken(IEnumerable<Claim> claims);
    }
}
