using System.ComponentModel;

namespace Tudo_List.Domain.Enums
{
    public enum Priority
    {
        [Description(nameof(Low))]
        Low,
        [Description(nameof(Medium))]
        Medium,
        [Description(nameof(High))]
        High
    }
}
