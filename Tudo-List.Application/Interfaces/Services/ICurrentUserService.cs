namespace Tudo_List.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string Id { get; }
        string Name { get; }
        string Email { get; }
    }
}
