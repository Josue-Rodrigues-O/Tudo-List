using FluentValidation;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            const string propertyUser = nameof(User);
            const string propertyName = nameof(User.Name);

            RuleFor(u => u.Name)
                    .NotEmpty()
                        .WithMessage(ValidationErrorMessages.NameIsEmpty)
                    .Length(ValidationConstants.NameMaximumLength, ValidationConstants.NameMaximumLength)
                        .WithMessage(ValidationHelper.GetEmptyPropertyMessage(propertyUser, propertyName));

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage()
        }
    }
}
