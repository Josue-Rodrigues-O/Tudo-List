using Tudo_List.Application.Models.Dtos;

namespace Tudo_List.Application.Interfaces
{
    public interface IAuthService
    {
        bool CheckPassword(int userId, string password);
    }
}
