using Tudo_List.Domain.Enums;

namespace Tudo_List.Application.Models.Dtos.TodoListItem
{
    public class AddItemDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
