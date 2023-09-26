using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public void GenerateToken(IEnumerable<Claim> claims)
        {
            try
            {
                LiturgyApiConfig liturgyApiConfig = new();

                var tokenHandler = new JwtSecurityTokenHandler();

                liturgyApiConfig.ApiSecret = _configuration.GetValue<string>("LiturgyApiConfig:ApiSecret");

                var key = Encoding.ASCII.GetBytes(liturgyApiConfig.ApiSecret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                string encryptedToken = tokenHandler.WriteToken(token);       
                
                SaveTokenOnCookie(encryptedToken);
            }
            catch(Exception ex)
            {
                throw new Exception(Exceptions.EXC21, ex);
            }
        }

        private void SaveTokenOnCookie(string encryptedToken)
        {
            try
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append("token_auth", encryptedToken,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddHours(3),
                        HttpOnly = true,
                        Secure = true,
                        IsEssential = true,
                        SameSite = SameSiteMode.None
                    });
            }
            catch(Exception ex)
            {
                throw new Exception(Exceptions.EXC23);
            }
        }
    }
}
