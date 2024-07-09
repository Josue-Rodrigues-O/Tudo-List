using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Validation.Attributes;
using Tudo_List.Domain.Validation.Constants;

namespace Tudo_List.Domain.Models.User
{
    public class UpdateUserRequest
    {
        [RequiredIntId]
        public int UserId { get; set; }

        public string? Name { get; set; }
    }
}
