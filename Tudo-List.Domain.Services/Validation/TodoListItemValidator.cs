using FluentValidation;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Services.Helpers;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services.Validation
{
    public class TodoListItemValidator : AbstractValidator<TodoListItem>
    {
        public TodoListItemValidator(ICurrentUserService currentUserService)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleSet(RuleSetNames.Register, ValidateItemTitle);
            RuleSet(RuleSetNames.Update, () =>
            {
                When(item => item.Title != null, ValidateItemTitle);

                ValidateUserId(currentUserService, RuleSetNames.Update);
            });

            RuleSet(RuleSetNames.Delete, () =>
            {
                ValidateUserId(currentUserService, RuleSetNames.Delete);
            });
        }

        private void ValidateItemTitle()
        {
            const int titleMinimumLength = TodoListItemValidationContants.TitleMinimumLength;
            const int titleMaximumLength = TodoListItemValidationContants.TitleMaximumLength;

            RuleFor(item => item.Title)
                .NotEmpty()
                    .WithMessage(ValidationMessageHelper.GetInvalidPropertyValueMessage("{PropertyName}", "{PropertyValue}"))
                .Length(titleMinimumLength, titleMaximumLength)
                    .WithMessage(ValidationMessageHelper.GetInvalidLengthMessage("{PropertyName}", titleMinimumLength, titleMaximumLength));
        }

        private void ValidateUserId(ICurrentUserService currentUserService, string operation)
        {
            RuleFor(item => item.UserId)
                .Equal(int.Parse(currentUserService.Id))
                .WithMessage(ValidationMessageHelper.GetUnauthorizedItemOperationMessage(operation));
        }
    }
}