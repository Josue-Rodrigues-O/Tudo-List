using Tudo_List.Application.Models.Dtos;

namespace Tudo_List.Application.Interfaces.Services
{
    public interface ITokenService
    {
        AuthResultDto GenerateToken(UserDto user);
    }
}
