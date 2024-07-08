namespace Tudo_List.Application.Interfaces.Services
{
    public interface IAuthService
    {
        bool CheckPassword(int userId, string password);
    }
}
