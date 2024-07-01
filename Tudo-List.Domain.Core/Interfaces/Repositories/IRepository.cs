namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Register(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
