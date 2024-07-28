using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Tudo_List.Domain.Helpers
{
    public static class StringHelper
    {
        public static bool ContainsValue(this string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static bool IsLengthBetween(this string? value, int minimumLength, int maximumLength)
        {
            if (value is null)
                return false;

            return value.Length >= minimumLength && value.Length <= maximumLength;
        }

        public static string TrimAndCondenseSpaces(this string? value)
        {
            if (value is null)
                return string.Empty;

            const char whiteSpace = ' ';
            //return Regex.Replace(value.Trim(), @"\s+", whiteSpace)
            return string.Join(whiteSpace, value.Split(whiteSpace, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
