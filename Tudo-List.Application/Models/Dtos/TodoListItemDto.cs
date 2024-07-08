using Tudo_List.Domain.Enums;

namespace Tudo_List.Application.Models.Dtos
{
    public class TodoListItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreationDate { get; set; }
        public int UserId { get; set; }
    }
}
