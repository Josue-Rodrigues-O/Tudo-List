namespace Tudo_List.Domain.Services.Helpers
{
    public static class ValidationHelper
    {
        public static string GetInvalidPropertyMessage(string property)
        {
            return $"{property} value is invalid!";
        }

        public static string GetEmptyPropertyMessage(string className, string property)
        {
            return $"The {className} {property} can't be Null, Empty String or Whitespace!";
        }

        public static string GetInvalidLengthMessage(string className, string property, int minLength, int maxLength)
        {
            return $"The {className} {property} must have between {minLength} and {maxLength} characters!";
        }
    }
}
