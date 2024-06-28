using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Entities
{
    public class TodoListItem
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
