using System.ComponentModel.DataAnnotations;

namespace Tudo_List.Application.Models.Users
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "The Username is Required!")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "The Name is Required!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Password is Required!")]
        public string Password {  get; set; }
    }
}
