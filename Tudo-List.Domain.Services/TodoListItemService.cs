using FluentValidation;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Domain.Helpers;
using Tudo_List.Domain.Models;
using Tudo_List.Domain.Services.Validation.Constants;

namespace Tudo_List.Domain.Services
{
    public class TodoListItemService(ITodoListItemRepository repository, IValidator<TodoListItem> itemValidator, ICurrentUserService currentUserService) : ITodoListItemService
    {
        private readonly ITodoListItemRepository _itemRepository = repository;
        private readonly IValidator<TodoListItem> _itemValidator = itemValidator;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        private int CurrentUserId => int.Parse(_currentUserService.Id);

        public IEnumerable<TodoListItem> GetAll(TodoListItemQueryFilter filter)
        {
            return _itemRepository.GetAll(CurrentUserId, filter);
        }

        public async Task<IEnumerable<TodoListItem>> GetAllAsync(TodoListItemQueryFilter filter)
        {
            return await _itemRepository.GetAllAsync(CurrentUserId, filter);
        }

        public TodoListItem? GetById(Guid id)
        {
            return _itemRepository.GetById(id, CurrentUserId);
        }

        public async Task<TodoListItem?> GetByIdAsync(Guid id)
        {
            return await _itemRepository.GetByIdAsync(id, CurrentUserId);
        }

        public void Add(TodoListItem item)
        {
            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            });

            item.UserId = CurrentUserId;
            item.Status ??= Status.NotStarted;
            item.CreationDate = DateTime.Now;

            _itemRepository.Add(item);
        }

        public async Task AddAsync(TodoListItem item)
        {
            await _itemValidator.ValidateAsync(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            });

            item.UserId = CurrentUserId;
            item.Status ??= Status.NotStarted;
            item.CreationDate = DateTime.Now;

            await _itemRepository.AddAsync(item);
        }

        public void Update(TodoListItem model)
        {
            var item = _itemRepository.GetById(model.Id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), model.Id);

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
                item.Status = ((int)model.Status).AsEnum<Status>();

            if (model.Priority is not null) 
                item.Priority = ((int)model.Priority).AsEnum<Priority>();

            _itemRepository.Update(item);
        }

        public async Task UpdateAsync(TodoListItem model)
        {
            var item = await _itemRepository.GetByIdAsync(model.Id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), model.Id);

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
                item.Status = ((int)model.Status).AsEnum<Status>();

            if (model.Priority is not null)
                item.Priority = ((int)model.Priority).AsEnum<Priority>();

            await _itemRepository.UpdateAsync(item);
        }

        public void Delete(Guid id)
        {
            var item = _itemRepository.GetById(id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Delete);
            });

            _itemRepository.Remove(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _itemRepository.GetByIdAsync(id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            _itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Delete);
            });

            await _itemRepository.RemoveAsync(item);
        }
    }
}
