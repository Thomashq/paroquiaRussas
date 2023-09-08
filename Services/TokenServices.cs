using Microsoft.IdentityModel.Tokens;
using paroquiaRussas.Models;
using paroquiaRussas.Utility;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Enum = paroquiaRussas.Utility.Enum;

namespace paroquiaRussas.Services
{
    public class TokenServices
    {
        public static string GenerateToken(Person person)
        {
            try
            {
                LiturgyApiConfig liturgyApiConfig = new();

                var tokenHandler = new JwtSecurityTokenHandler();
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

                return tokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                throw new Exception("Não foi possível gerar um token corretamente", ex);
            }
        }
    }
}
