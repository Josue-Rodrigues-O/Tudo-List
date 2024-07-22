using Tudo_List.Application.Models.Dtos.User;

namespace Tudo_List.Application.DtoValidation.Helpers
{
    public static class ApplicationValidationHelper
    {
        private static bool IsCurrentUser(int currentUserId, int userId) 
            => currentUserId == userId;

        public static bool IsValidPasswordUpdate(int currentUserId, UpdatePasswordDto model)
            => IsCurrentUser(currentUserId, model.UserId) && model.NewPassword == model.ConfirmNewPassword;

        public static bool IsValidEmailUpdate(int currentUserId, UpdateEmailDto model)
            => IsCurrentUser(currentUserId, model.UserId);

        public static bool IsValidUserUpdate(int currentUserId, UpdateUserDto model)
            => IsCurrentUser(currentUserId, model.UserId) && model.NewName is not null;
    }
}
