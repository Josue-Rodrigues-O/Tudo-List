namespace Tudo_List.Domain.Core.Interfaces.Configuration
{
    public interface ISecrets
    {
        string SqlServerConnectionString { get; }
        string JwtPrivateKey { get; }
    }
}
