using System.Xml.Linq;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Validation;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public class UserValidator(IUserRepository userRepository) : IUserValidator
    {
        private readonly IUserRepository _userRepository = userRepository;
        private Dictionary<string, object> _userProperties = new();

        public UserValidator WithName(string name)
        {
            _userProperties[UserValidationConstants.NameProperty] = name;
            return this;
        }

        public UserValidator WithEmail(string email)
        {
            _userProperties[UserValidationConstants.EmailProperty] = email;
            return this;
        }

        public UserValidator WithPassword(string password)
        {
            _userProperties[UserValidationConstants.PasswordProperty] = password;
            return this;
        }

        public void Validate()
        {
            foreach (var property in _userProperties.Keys)
            {

            }
        }
    }
}
