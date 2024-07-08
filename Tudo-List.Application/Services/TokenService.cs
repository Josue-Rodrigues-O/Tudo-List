using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos;
using Tudo_List.Application.Models.Dtos.Login;
using Tudo_List.Domain.Core.Interfaces.Configuration;

namespace Tudo_List.Application.Services
{
    public class TokenService(ISecrets secrets) : ITokenService
    {
        private readonly ISecrets _secrets = secrets;

        public AuthResultDto GenerateToken(UserDto user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secrets.JwtPrivateKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = GenerateClaims(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(8),
            };

            var token = handler.CreateToken(tokenDescriptor);
            var strToken = handler.WriteToken(token);

            return new AuthResultDto()
            {
                Success = true,
                Token = strToken,
            };
        }

        private static ClaimsIdentity GenerateClaims(UserDto user)
        {
            return new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
            ]);
        }
    }
}
