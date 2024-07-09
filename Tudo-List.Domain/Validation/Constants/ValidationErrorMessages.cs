using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models.TodoListItem;
using Tudo_List.Domain.Models.User;

namespace Tudo_List.Domain.Validation.Constants
{
    public static class ValidationErrorMessages
    {
        public const string RequiredName = nameof(User.Name) + isRequired;
        public const string RequiredEmail = nameof(User.Email) + isRequired;
        public const string RequiredNewEmail = nameof(User.Email) + isRequired;
        public const string RequiredPassword = nameof(RegisterUserRequest.Password) + isRequired;
        
        public const string RequiredTitle = nameof(TodoListItem.Title) + isRequired;

        public const string RequiredUserId = nameof(UpdateEmailRequest.UserId) + isRequired;
        public const string RequiredCurrentPassword = nameof(UpdateEmailRequest.CurrentPassword) + isRequired;
        public const string RequiredItemId = nameof(UpdateItemRequest.ItemId) + isRequired;
        
        public const string RequiredNewPassword = nameof(UpdatePasswordRequest.NewPassword) + isRequired;
        public const string RequiredConfirmNewPassword = nameof(UpdatePasswordRequest.ConfirmNewPassword) + isRequired;

        private const string isRequired = " is required for this operation!";
    }
}
