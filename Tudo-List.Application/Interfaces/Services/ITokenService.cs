using Tudo_List.Application.Models.Dtos;
using Tudo_List.Application.Models.Dtos.Login;

namespace Tudo_List.Application.Interfaces.Services
{
    public interface ITokenService
    {
        AuthResultDto GenerateToken(UserDto user);
    }
}
