using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Validation;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public partial class UserValidator : IUserValidator
    {
        private readonly IUserRepository _userRepository;
        private List<string> _errors;

        private string? _name;
        private string? _email;
        private string? _password;

        public UserValidator() { }
        
        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IUserValidator WithName(string name)
        { 
            _name = name ?? string.Empty;
            return this;
        }

        public IUserValidator WithEmail(string email)
        {
            _email = email ?? string.Empty;
            return this;
        }

        public IUserValidator WithPassword(string password)
        {
            _password = password ?? string.Empty;
            return this;
        }

        public void Validate()
        {
            _errors = [];

            if (_name is not null)
                ValidateName(_name);

            if (_email is not null)
                ValidateEmail(_email);

            if (_password is not null)
                ValidatePassword(_password);

            if (_errors.Count != uint.MinValue)
                throw new ValidationException(string.Join('\n', _errors));
        }

        private void ValidateName(string name)
        {
            const string nameProperty = nameof(User.Name);

            if (string.IsNullOrWhiteSpace(name))
            {
                _errors.Add(ValidationHelper.GetInvalidPropertyValueMessage(nameProperty, name));
                return;
            }

            var nameWithValidFormat = name.TrimAndCondenseSpaces();

            const int minimumLength = UserValidationConstants.NameMinimumLength;
            const int maximumLength = UserValidationConstants.NameMaximumLength;

            if (!nameWithValidFormat.IsLengthBetween(minimumLength, maximumLength))
                _errors.Add(ValidationHelper.GetInvalidLengthMessage(nameof(nameProperty), minimumLength, maximumLength));
        }

        private void ValidateEmail(string email)
        {
            if (_userRepository is null)
                throw new ArgumentNullException("You must inject the repository dependency via constructor to validate email!");

            if (string.IsNullOrWhiteSpace(email))
            {
                _errors.Add(ValidationHelper.GetInvalidPropertyValueMessage(UserValidationConstants.EmailProperty, email));
                return;
            }

            var emailWithValidFormat = email.TrimAndCondenseSpaces();

            if (!ValidEmailRegex().IsMatch(emailWithValidFormat))
                _errors.Add(ValidationHelper.GetInvalidFormatMessage(UserValidationConstants.EmailProperty));
            else if (_userRepository.GetByEmail(emailWithValidFormat) != null)
                _errors.Add(ValidationHelper.GetUniquePropertyMessage(UserValidationConstants.EmailProperty));
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                _errors.Add(ValidationHelper.GetInvalidPropertyValueMessage(UserValidationConstants.PasswordProperty, password));
                return;
            }

            const int minimumLength = UserValidationConstants.PasswordMinimumLength;
            const int maximumLength = UserValidationConstants.PasswordMaximumLength;

            var passwordWithValidFormat = password.TrimAndCondenseSpaces();

            if (!passwordWithValidFormat.IsLengthBetween(minimumLength, maximumLength))
                _errors.Add(ValidationHelper.GetInvalidLengthMessage(UserValidationConstants.PasswordProperty, minimumLength, maximumLength));
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex ValidEmailRegex();
    }
}
