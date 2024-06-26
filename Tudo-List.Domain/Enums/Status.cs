using System.ComponentModel;

namespace Tudo_List.Domain.Enums
{
    public enum Status
    {
        [Description("Not Started")]
        NotStarted,
        [Description("In Progress")]
        InProgress,
        [Description(nameof(Completed))]
        Completed
    }
}
