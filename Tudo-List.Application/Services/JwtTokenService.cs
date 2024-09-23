using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tudo_List.Application.Dtos.User;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Domain.Core.Interfaces.Configuration;

namespace Tudo_List.Application.Services
{
    public class JwtTokenService(ISecrets secrets) : ITokenService
    {
        private readonly ISecrets _secrets = secrets;

        public string GenerateToken(UserDto user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secrets.JwtPrivateKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddYears(100)//.AddHours(8),
            };

            var token = handler.CreateToken(tokenDescriptor);
            var strToken = handler.WriteToken(token);

            return strToken;
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secrets.JwtPrivateKey);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static ClaimsIdentity GenerateClaims(UserDto user)
        {
            return new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            ]);
        }
    }
}
