using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Interfaces;
using paroquiaRussas.Utility.Resources;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace paroquiaRussas.Services
{
    public class TokenServices : IToken
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenServices(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public dynamic GenerateToken(IEnumerable<Claim> claims)
        {
            try
            {
                LiturgyApiConfig liturgyApiConfig = new();

                var tokenHandler = new JwtSecurityTokenHandler();

                liturgyApiConfig.ApiSecret = _configuration.GetValue<string>("LiturgyApiConfig:ApiSecret");

                var key = Encoding.ASCII.GetBytes(liturgyApiConfig.ApiSecret);

                //Token vai expirar a cada 3h, será usado para definir se o usuário está logado
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                string encryptedToken = tokenHandler.WriteToken(token);

                _httpContextAccessor.HttpContext.Response.Cookies.Append("token", encryptedToken,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddHours(3),
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        SameSite = SameSiteMode.None
                    });

                return new {Messages.MSG11};
            }
            catch(Exception ex)
            {
                throw new Exception(Exceptions.EXC21, ex);
            }
        }

        public string GenerateRefreshToken()
        {
            try
            {
                var randomNumber = new byte[32];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            try
            {
                LiturgyApiConfig liturgyApiConfig = new LiturgyApiConfig();
                    
                liturgyApiConfig.ApiSecret = _configuration.GetValue<string>("LiturgyApiConfig:ApiSecret");

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(liturgyApiConfig.ApiSecret)),
                    ValidateLifetime = false
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (securityToken is not JwtSecurityToken jwtSecurityToken || 
                    !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                        throw new SecurityTokenException(Exceptions.EXC22);

                return principal;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
