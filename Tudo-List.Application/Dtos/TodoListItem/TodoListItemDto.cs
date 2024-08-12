using Tudo_List.Domain.Enums;

namespace Tudo_List.Application.Dtos.TodoListItem
{
    public record TodoListItemDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string? Description { get; init; }
        public Status Status { get; init; }
        public Priority Priority { get; init; }
        public DateTime CreationDate { get; init; }
    }
}
