using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Models
{
    public record TodoListItemQueryFilter
    {
        public string? Title { get; init; }
        public Status? Status { get; init; }
        public Priority? Priority { get; init; }
        public DateTime? CreationDate { get; init; }
        public DateTime? InitialDate { get; init; }
        public DateTime? FinalDate { get; init; }
    }
}
