using System.ComponentModel;

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

        public static T AsEnum<T>(this int value) where T : struct, Enum
        {
            const string enumName = nameof(T);

            if (!Enum.IsDefined(typeof(T), value))
            {
                throw new ArgumentOutOfRangeException(enumName, $"{value} is out of range for Enum {enumName}");
            }
            return (T)(object)value;
        }
    }
}
