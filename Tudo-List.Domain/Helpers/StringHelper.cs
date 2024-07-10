namespace Tudo_List.Domain.Helpers
{
    public static class StringHelper
    {
        public static bool ContainsValue(this string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
