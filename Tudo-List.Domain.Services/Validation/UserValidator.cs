using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Validation;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public partial class UserValidator(IUserRepository userRepository) : IUserValidator
    {
        private readonly IUserRepository _userRepository = userRepository;
        private List<string> _errors = [];

        private string _name;
        private string _email;
        private string _password;
        
        public IUserValidator WithName(string name)
        {
            _name = name;
            return this;
        }

        public IUserValidator WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public IUserValidator WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public void Validate()
        {
            _errors.Clear();

            if (_name.ContainsValue())
                ValidateName(_name);

            if (_email.ContainsValue())
                ValidateEmail(_email);

            if (_password.ContainsValue())
                ValidatePassword(_password);

            if (_errors.Count != uint.MinValue)
                throw new ValidationException(string.Join('\n', _errors));
            
            ResetValues();
        }

        private void ValidateName(string name)
        {
            var nameWithValidFormat = name.TrimAndCondenseSpaces();

            const int minimumLength = UserValidationConstants.NameMinimumLength;
            const int maximumLength = UserValidationConstants.NameMaximumLength;

            if (!nameWithValidFormat.IsLengthBetween(minimumLength, maximumLength))
                _errors.Add(ValidationHelper.GetInvalidLengthMessage(nameof(User.Name), minimumLength, maximumLength));
        }

        private void ValidateEmail(string email)
        {
            var emailWithValidFormat = email.TrimAndCondenseSpaces();

            if (!ValidEmailRegex().IsMatch(emailWithValidFormat))
                _errors.Add(ValidationHelper.GetInvalidFormatMessage(nameof(User.Email)));
            else if (_userRepository.GetByEmail(emailWithValidFormat) != null)
                _errors.Add(ValidationHelper.GetUniquePropertyMessage(nameof(User.Email)));
        }

        private void ValidatePassword(string password)
        {
            const string passwordProperty = "Password";
            const int minimumLength = UserValidationConstants.PasswordMaximumLength;
            const int maximumLength = UserValidationConstants.PasswordMaximumLength;

            var passwordWithValidFormat = password.TrimAndCondenseSpaces();

            if (!passwordWithValidFormat.IsLengthBetween(minimumLength, maximumLength))
                _errors.Add(ValidationHelper.GetInvalidLengthMessage(passwordProperty, minimumLength, maximumLength));
        }

        private void ResetValues()
        {
            _name = string.Empty;
            _email = string.Empty;
            _password = string.Empty;
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex ValidEmailRegex();
    }
}
