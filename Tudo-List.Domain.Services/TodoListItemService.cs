using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Domain.Services
{
    public class TodoListItemService(ITodoListItemRepository repository) : ITodoListItemService
    {
        private readonly ITodoListItemRepository _repository = repository;

        public IEnumerable<TodoListItem> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<IEnumerable<TodoListItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public TodoListItem GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public async Task<TodoListItem> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Add(TodoListItem item)
        {
            _repository.Add(item);
        }

        public async Task AddAsync(TodoListItem item)
        {
            await _repository.AddAsync(item);
        }

        public void Update(TodoListItem model)
        {
            var item = _repository.GetById(model.Id);

            item.Title = model.Title ?? item.Title;
            item.Description = model.Description ?? item.Description;
            item.Status = model.Status ?? item.Status;
            item.Priority = model.Priority ?? item.Priority;

            _repository.Update(item);
        }

        public async Task UpdateAsync(TodoListItem model)
        {
            var item = await _repository.GetByIdAsync(model.Id);

            item.Title = model.Title ?? item.Title;
            item.Description = model.Description ?? item.Description;
            item.Status = model.Status ?? item.Status;
            item.Priority = model.Priority ?? item.Priority;

            await _repository.UpdateAsync(item);
        }

        public void Delete(Guid id)
        {
            _repository.Remove(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.RemoveAsync(id);
        }
    }
}
