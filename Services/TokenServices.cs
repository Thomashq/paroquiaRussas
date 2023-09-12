using Microsoft.IdentityModel.Tokens;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using paroquiaRussas.Utility.Interfaces;
using paroquiaRussas.Utility.Resources;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using Enum = paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Services
{
    public class TokenServices : IToken
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration config)
        {
            _configuration = config;
        }

        public string GenerateToken(Person person)
        {
            try
            {
                LiturgyApiConfig liturgyApiConfig = new();

                var tokenHandler = new JwtSecurityTokenHandler();

                liturgyApiConfig.ApiSecret = _configuration.GetValue<string>("LiturgyApiConfig:ApiSecret");

                var key = Encoding.ASCII.GetBytes(liturgyApiConfig.ApiSecret);
                string role = Enum.GetEnumDescription(person.Role);
                
                //Token vai expirar a cada 3h, será usado para definir se o usuário está logado
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, person.Id.ToString()),
                        new Claim(ClaimTypes.Role, role),
                        new Claim("Chave", "b5d8a1f7")
                    }),

                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                string tokenTest =  tokenHandler.WriteToken(token);

                return tokenTest;
            }
            catch(Exception ex)
            {
                throw new Exception(Exceptions.EXC21, ex);
            }
        }
    }
}
