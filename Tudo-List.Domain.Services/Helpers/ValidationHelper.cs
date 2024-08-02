namespace Tudo_List.Domain.Services.Helpers
{
    public static class ValidationHelper
    {
        public static string GetInvalidPropertyValueMessage(string property, object? value)
        {
            var message = string.IsNullOrWhiteSpace(value?.ToString())
                ? $"The field {property} can't be empty!"
                : $"{value} value is invalid for field {property}!";

            return message;
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

        public static string GetUnauthorizedOperationMessage(string entity, string operation)
        {
            return $"The current {entity} is not authorized to execute {operation} on another {entity}!";
        }

        public static string GetMustBeEqualMessage(string parameter1, string parameter2)
        {
            return $"{parameter1} must be equal to {parameter2}!";
        }

        public static string GetCantBeTheSameAsCurrentPropertyMessage(string property)
        {
            return $"The new {property} can't be the same as the current one!";
        }
    }
}
