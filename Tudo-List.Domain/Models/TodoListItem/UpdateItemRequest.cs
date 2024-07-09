using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Validation.Attributes;

namespace Tudo_List.Domain.Models.TodoListItem
{
    public class UpdateItemRequest
    {
        [RequireGuidId]
        public Guid ItemId { get; set; }
        
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Status? Status { get; set; }
        public Priority? Priority { get; set; }
    }
}
