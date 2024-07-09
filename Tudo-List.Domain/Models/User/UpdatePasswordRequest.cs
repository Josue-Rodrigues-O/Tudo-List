namespace Tudo_List.Domain.Models.User
{
    public class UpdatePassword
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
