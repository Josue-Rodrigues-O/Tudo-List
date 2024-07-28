using System.ComponentModel;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Domain.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            var description = value.ToString();
            var fieldInfo = value.GetType().GetField(description);
            var attrs = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), true);

            if (attrs != null && attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }

            return description;
        }

        public static T GetRandomValue<T>() where T : struct, Enum
        {
            T[] enumValues = (T[])Enum.GetValues(typeof(T));
            var index = new Random().Next(enumValues.Length);

            return enumValues[index];
        }

        public static Status ParseStatus(int value)
        {
            if (!Enum.IsDefined(typeof(Status), value))
            {
                throw new ArgumentOutOfRangeException(nameof(TodoListItem.Status), $"{value} is not a valid status");
            }
            return (Status)value;
        }

        public static Priority ParsePriority(int value)
        {
            if (!Enum.IsDefined(typeof(Priority), value))
            {
                throw new ArgumentOutOfRangeException(nameof(TodoListItem.Priority), $"{value} is not a valid status");
            }
            return (Priority)value;
        }
    }
}
