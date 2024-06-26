using System.ComponentModel;

namespace Tudo_List.Domain.Extensions
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
    }
}
