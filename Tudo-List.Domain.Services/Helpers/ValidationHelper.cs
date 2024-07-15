namespace Tudo_List.Domain.Services.Helpers
{
    public static class ValidationHelper
    {
        public static string GetInvalidPropertyMessage(string property)
        {
            return $"{property} value is invalid!";
        }

        public static string GetEmptyPropertyMessage(string property)
        {
            return $"{property} must have a value!";
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
