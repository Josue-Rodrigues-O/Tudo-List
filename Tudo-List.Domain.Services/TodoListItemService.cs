using FluentValidation;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services
{
    public class TodoListItemService(ITodoListItemRepository repository, IValidator<TodoListItem> itemValidator) : ITodoListItemService
    {
        private readonly ITodoListItemRepository _repository = repository;
        private readonly IValidator<TodoListItem> _itemValidator = itemValidator;

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

        public void Add(TodoListItem item, int userId)
        {
            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            });

            item.UserId = userId;
            item.Status ??= Status.NotStarted;
            item.CreationDate = DateTime.Now;

            _repository.Add(item);
        }

        public async Task AddAsync(TodoListItem item, int userId)
        {
            await _itemValidator.ValidateAsync(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            });

            item.UserId = userId;
            item.Status ??= Status.NotStarted;
            item.CreationDate = DateTime.Now;

            await _repository.AddAsync(item);
        }

        public void Update(TodoListItem model)
        {
            var item = _repository.GetById(model.Id);

            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Update);
            });

            if (model.Title is not null)
                item.Title = model.Title;

            if (model.Description is not null)
                item.Description = model.Description;

            if (model.Status is not null) 
                item.Status = model.Status;

            if (model.Priority is not null) 
                item.Priority = model.Priority;

            _repository.Update(item);
        }

        public async Task UpdateAsync(TodoListItem model)
        {
            var item = await _repository.GetByIdAsync(model.Id);

            await _itemValidator.ValidateAsync(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Update);
            });

            if (model.Title is not null)
                item.Title = model.Title;

            if (model.Description is not null)
                item.Description = model.Description;

            if (model.Status is not null)
                item.Status = model.Status;

            if (model.Priority is not null)
                item.Priority = model.Priority;

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
