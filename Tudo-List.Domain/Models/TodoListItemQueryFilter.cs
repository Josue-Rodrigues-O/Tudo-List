using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Models
{
    public record TodoListItemQueryFilter(string? Title, Status? Status, Priority? Priority, DateTime? CreationDate, DateTime? InitialDate, DateTime? FinalDate);
}
