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
            return value != null && value.Length >= minimumLength && value.Length <= maximumLength;
        }

        public static string TrimAndCondenseSpaces(this string? value)
        {
            if (value == null)
                return string.Empty;

            var allWords = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            //Regex.Replace(value.Trim(), @"\s+", ' ')
            return string.Join(' ', allWords);
        }
    }
}
