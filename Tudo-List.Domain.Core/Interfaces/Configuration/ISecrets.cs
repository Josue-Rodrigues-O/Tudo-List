namespace Tudo_List.Domain.Core.Interfaces.Configuration
{
    public interface ISecrets
    {
        string ConnectionString { get; }
        string JwtPrivateKey { get; }
    }
}
