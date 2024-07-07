namespace Tudo_List.Domain.Core.Interfaces.Strategies
{
    public interface IPasswordStrategy
    {
        bool UsesSalting { get; }
        string HashPassword(string password, string? salt = null);
        bool VerifyPassword(string password, string passwordHash, string? salt = null);
    }
}
