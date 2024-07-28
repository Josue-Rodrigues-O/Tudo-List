namespace Tudo_List.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string entity, string propertyName, object? propertyValue) : base($"{entity} was not found with {propertyName} {propertyValue}!")
        {
        }
    }
}
