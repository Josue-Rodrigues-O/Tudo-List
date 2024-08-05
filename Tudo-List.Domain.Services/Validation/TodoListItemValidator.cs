using FluentValidation;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public class TodoListItemValidator : AbstractValidator<TodoListItem>
    {
        private readonly ICurrentUserService _currentUser;

        public TodoListItemValidator(ICurrentUserService currentUserService)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            _currentUser = currentUserService;

            RuleSet(RuleSetNames.Register, ValidateItemTitle);
            RuleSet(RuleSetNames.Update, () =>
            {
                When(item => item.Title is not null, ValidateItemTitle);

                ValidateUserId(RuleSetNames.Update);
            });

            RuleSet(RuleSetNames.Delete, () =>
            {
                ValidateUserId(RuleSetNames.Delete);
            });
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

        private void ValidateUserId(string operation)
        {
            var userId = int.Parse(_currentUser.Id);

            RuleFor(item => item.UserId)
                .Equal(userId)
                    .WithMessage(ValidationHelper.GetUnauthorizedItemOperationMessage(operation));
        }
    }
}