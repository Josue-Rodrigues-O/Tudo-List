using Tudo_List.Domain.Core.Interfaces.Configuration;

namespace Tudo_List.Test.Mock
{
    internal class SecretsMock : ISecrets
    {
        public string ConnectionString => string.Empty;

        public string JwtPrivateKey => "Z3UtdUfhX7u6n7jL3kW7W9m05ADlB2pzFeucA/Ewk/k=";
    }
}
