using Microsoft.Extensions.Configuration;
using Tudo_List.Domain.Core.Interfaces.Configuration;

namespace Tudo_list.Infrastructure.Configuration.Constants
{
    public class Secrets(IConfiguration configuration) : ISecrets
    {
        private readonly IConfiguration _configuration = configuration;

        public string SqlServerConnectionString => _configuration.GetConnectionString(SecretsKeys.SqlServerConnectionString)
            ?? throw new ArgumentNullException(nameof(SqlServerConnectionString), "The application does not have a connection string for Sql Server!");

        public string JwtPrivateKey => _configuration.GetSection(SecretsKeys.JwtPrivateKey).Value
            ?? throw new ArgumentNullException(nameof(JwtPrivateKey), "The application does not have a private key for JWT!");
    }
}
