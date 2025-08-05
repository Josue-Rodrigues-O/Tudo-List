using System.ComponentModel.DataAnnotations;
using Tudo_List.Application.DtoValidation;

namespace Tudo_List.Application.Dtos.User
{
    public record UpdateEmailDto([RequiredProperty] int UserId, [RequiredProperty][EmailAddress] string NewEmail, [RequiredProperty] string CurrentPassword);
}
