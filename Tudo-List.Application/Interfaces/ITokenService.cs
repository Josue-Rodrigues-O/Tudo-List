using Tudo_List.Application.Models.Dtos;

namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface ITokenService
    {
        AuthResultDto GenerateToken(UserDto user);
    }
}
