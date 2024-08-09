using Tudo_List.Application.DtoValidation;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Application.Dtos.TodoListItem
{
    public record AddItemDto
    {
        [RequiredProperty]
        public string Title { get; init; }

        public string? Description { get; init; }
        public Status Status { get; init; }
        public Priority Priority { get; init; }
    }
}
