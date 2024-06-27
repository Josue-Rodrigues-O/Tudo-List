namespace Tudo_List.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
