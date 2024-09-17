using FluentValidation;
using FluentValidation.Results;
using System.Text.RegularExpressions;
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
        private ICollection<ValidationFailure> _validationFailures;

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
            _validationFailures = [];

            if (_name is not null)
                ValidateName(_name);

            if (_email is not null)
                ValidateEmail(_email);

            if (_password is not null)
                ValidatePassword(_password);

            if (_validationFailures.Count != uint.MinValue)
            {
                throw new ValidationException(_validationFailures);
            }
        }

        private void ValidateName(string name)
        {
            const string nameProperty = nameof(User.Name);

            if (string.IsNullOrWhiteSpace(name))
            {
                _validationFailures.Add(new ValidationFailure(nameProperty, ValidationMessageHelper.GetInvalidPropertyValueMessage(nameProperty, name)));
                return;
            }

            const int minimumLength = UserValidationConstants.NameMinimumLength;
            const int maximumLength = UserValidationConstants.NameMaximumLength;

            if (!name.TrimAndCondenseSpaces().IsLengthBetween(minimumLength, maximumLength))
                _validationFailures.Add(new ValidationFailure(nameProperty ,ValidationMessageHelper.GetInvalidLengthMessage(nameProperty, minimumLength, maximumLength)));
        }

        private void ValidateEmail(string email)
        {
            const string emailProperty = nameof(User.Email);

            if (_userRepository is null)
                throw new ArgumentNullException(nameof(_userRepository), "You must inject the repository dependency via constructor to validate email!");

            if (string.IsNullOrWhiteSpace(email))
            {
                _validationFailures.Add(new ValidationFailure());
                return;
            }

            var emailWithValidFormat = email.TrimAndCondenseSpaces();

            if (!ValidEmailRegex().IsMatch(emailWithValidFormat))
                _validationFailures.Add(new ValidationFailure(emailProperty, ValidationMessageHelper.GetInvalidFormatMessage(emailProperty)));
            else if (_userRepository.GetByEmail(emailWithValidFormat) != null)
                _validationFailures.Add(new ValidationFailure(emailProperty,ValidationMessageHelper.GetUniquePropertyMessage(emailProperty)));
        }

        private void ValidatePassword(string password)
        {
            const string passwordProperty = "Password";

            if (string.IsNullOrWhiteSpace(password))
            {
                _validationFailures.Add(new ValidationFailure(passwordProperty, ValidationMessageHelper.GetInvalidPropertyValueMessage(passwordProperty, password)));
                return;
            }

            const int minimumLength = UserValidationConstants.PasswordMinimumLength;
            const int maximumLength = UserValidationConstants.PasswordMaximumLength;

            var passwordWithValidFormat = password.TrimAndCondenseSpaces();

            if (!passwordWithValidFormat.IsLengthBetween(minimumLength, maximumLength))
                _validationFailures.Add(new ValidationFailure(passwordProperty ,ValidationMessageHelper.GetInvalidLengthMessage(passwordProperty, minimumLength, maximumLength)));
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex ValidEmailRegex();
    }
}
