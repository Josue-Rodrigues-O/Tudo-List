namespace Tudo_List.Domain.Services.Helpers
{
    public static class ValidationHelper
    {
        public static string GetInvalidPropertyMessage(string property)
        {
            return $"{property} value is invalid!";
        }
    }
}
