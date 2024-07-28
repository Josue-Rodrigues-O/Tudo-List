using Tudo_List.Application.Models.Dtos.Login;
using Tudo_List.Application.Models.Dtos.User;

namespace Tudo_List.Application.Interfaces.Services
{
    public interface ITokenService
    {
        AuthResultDto GenerateToken(UserDto user);
    }
}
