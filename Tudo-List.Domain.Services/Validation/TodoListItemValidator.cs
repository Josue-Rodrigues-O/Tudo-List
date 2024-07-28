using FluentValidation;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public class TodoListItemValidator : AbstractValidator<TodoListItem>
    {
        public TodoListItemValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet(RuleSetNames.Register, ValidateItemTitle);

            RuleSet(RuleSetNames.Update, () => 
                When(item => item.Title is not null, ValidateItemTitle));
        }

        private void ValidateItemTitle()
        {
            const string propertyName = "{PropertyName}";
            const string propertyValue = "{PropertyValue}";

            const int titleMinimumLength = TodoListItemValidationContants.TitleMinimumLength;
            const int titleMaximumLength = TodoListItemValidationContants.TitleMaximumLength;

            RuleFor(item => item.Title)
                    .NotEmpty()
                        .WithMessage(ValidationHelper.GetInvalidPropertyValueMessage(propertyName, propertyValue))
                    .Length(titleMinimumLength, titleMaximumLength)
                        .WithMessage(ValidationHelper.GetInvalidLengthMessage(propertyName, titleMinimumLength, titleMaximumLength));
        }
    }
}