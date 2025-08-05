namespace Tudo_List.Domain.Core.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string Id { get; }
        string Name { get; }
        string Email { get; }
    }
}
