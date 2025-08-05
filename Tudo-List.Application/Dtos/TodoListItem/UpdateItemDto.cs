using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Application.Dtos.TodoListItem
{
    public record UpdateItemDto([RequiredProperty] Guid Id, string? Title, string? Description, Status? Status, Priority? Priority);
}
