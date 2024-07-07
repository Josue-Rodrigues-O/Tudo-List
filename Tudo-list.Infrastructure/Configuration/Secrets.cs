using Microsoft.Extensions.Configuration;
using Tudo_List.Domain.Core.Interfaces.Configuration;

namespace Tudo_list.Infrastructure.Configuration
{
    public class Secrets(IConfiguration configuration) : ISecrets
    {
        private readonly IConfiguration _configuration = configuration;

        public string SqlServerConnectionString => _configuration.GetConnectionString("SqlServer") 
            ?? throw new Exception("The application does not have a connection string for Sql Server!");
        
        public string JwtPrivateKey => _configuration.GetSection("JwtSettings:PrivateKey").Value
            ?? throw new Exception("The application does not have a private key for JWT!");
    }
}
