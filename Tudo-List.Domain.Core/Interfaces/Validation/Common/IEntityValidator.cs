namespace Tudo_List.Domain.Core.Interfaces.Validation.Common
{
    public interface IEntityValidator<T> where T : class, new()
    {
        void Validate(T entity);
    }
}
