namespace Tudo_List.Domain.Services.Helpers
{
    public static class ValidationHelper
    {
        public static string GetInvalidPropertyValueMessage(string property, object? value)
        {
            return $"\"{value}\" value is invalid for property {property}!";
        }

        public static string GetInvalidLengthMessage(string property, int minLength, int maxLength)
        {
            return $"{property} must have between {minLength} and {maxLength} characters!";
        }

        public static string GetInvalidFormatMessage(string property)
        {
            return $"{property} is in an incorrect format!";
        }

        public static string GetUniquePropertyMessage(string property)
        {
            return $"This {property} is already being used!";
        }
    }
}
