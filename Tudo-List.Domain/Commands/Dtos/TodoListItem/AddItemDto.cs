using System.ComponentModel.DataAnnotations;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Validation.Constants;

namespace Tudo_List.Domain.Commands.Dtos.TodoListItem
{
    public class AddItemDto
    {
        [Required(ErrorMessage = ValidationErrorMessages.RequiredTitle)]
        public string Title { get; set; }

        public string? Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }
}
