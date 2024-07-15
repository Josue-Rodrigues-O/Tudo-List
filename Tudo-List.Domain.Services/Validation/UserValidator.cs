using FluentValidation;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Validation;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public class UserValidator : AbstractValidator<User>, IUserValidator
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            const string propertyName = nameof(User.Name);
            const string propertyEmail = nameof(User.Email);

            RuleFor(u => u.Name)
                    .NotEmpty()
                        .WithMessage(ValidationHelper.GetEmptyPropertyMessage(propertyName))
                    .Length(ValidationConstants.NameMinimumLength, ValidationConstants.NameMaximumLength)
                        .WithMessage(ValidationHelper.GetInvalidLengthMessage(propertyName, ValidationConstants.NameMinimumLength, ValidationConstants.NameMaximumLength));


            RuleFor(u => u.Email)
                .NotEmpty()
                    .WithMessage(ValidationHelper.GetEmptyPropertyMessage(propertyEmail))
                .EmailAddress()
                    .WithMessage(ValidationHelper.GetInvalidFormatMessage(propertyEmail))
                .Must(EmailIsUnique)
                    .WithMessage("User Email Must be unique!");
        }

        private bool EmailIsUnique(string email)
            => _userRepository.GetByEmail(email) == null;
    }
}
