using Tudo_List.Domain.Enums;

namespace Tudo_List.Application.Dtos.TodoListItem
{
    public record TodoListItemDto(Guid Id, string Title, string? Description, Status Status, Priority Priority, DateTime CreationDate);
}
