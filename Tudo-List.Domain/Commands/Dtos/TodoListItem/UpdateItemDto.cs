using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Commands.Dtos.TodoListItem
{
    public class UpdateItemDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status? Status { get; set; }
        public Priority? Priority { get; set; }
    }
}
