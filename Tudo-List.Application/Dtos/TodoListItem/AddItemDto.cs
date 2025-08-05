using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Application.Dtos.TodoListItem
{
    public record AddItemDto([RequiredProperty] string Title, string? Description, Status Status, Priority Priority);
}
