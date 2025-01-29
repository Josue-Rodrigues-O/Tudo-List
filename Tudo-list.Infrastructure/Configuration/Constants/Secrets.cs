using Microsoft.Extensions.Configuration;
using Tudo_List.Domain.Core.Interfaces.Configuration;

namespace Tudo_list.Infrastructure.Configuration.Constants
{
    public class Secrets(IConfiguration configuration) : ISecrets
    {
        public string ConnectionString => configuration[SecretsKeys.SqlServerConnectionString]
            ?? throw new ArgumentNullException(nameof(ConnectionString), "The application does not have a connection string for Sql Server!");

        public string JwtPrivateKey => configuration[SecretsKeys.JwtPrivateKey]
            ?? throw new ArgumentNullException(nameof(JwtPrivateKey), "The application does not have a private key for JWT!");
    }
}
