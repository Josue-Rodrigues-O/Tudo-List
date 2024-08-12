using Tudo_List.Application.Dtos.User;

namespace Tudo_List.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}
