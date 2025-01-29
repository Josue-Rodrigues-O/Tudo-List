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
    public class TodoListItemService(ITodoListItemRepository itemRepository, IValidator<TodoListItem> itemValidator, ICurrentUserService currentUserService) : ITodoListItemService
    {
        private int CurrentUserId => int.Parse(currentUserService.Id);

        public IEnumerable<TodoListItem> GetAll(TodoListItemQueryFilter filter)
        {
            return itemRepository.GetAll(CurrentUserId, filter);
        }

        public async Task<IEnumerable<TodoListItem>> GetAllAsync(TodoListItemQueryFilter filter)
        {
            return await itemRepository.GetAllAsync(CurrentUserId, filter);
        }

        public TodoListItem? GetById(Guid id)
        {
            return itemRepository.GetById(id, CurrentUserId);
        }

        public async Task<TodoListItem?> GetByIdAsync(Guid id)
        {
            return await itemRepository.GetByIdAsync(id, CurrentUserId);
        }

        public void Add(TodoListItem item)
        {
            itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            });

            item.UserId = CurrentUserId;
            item.Status ??= Status.NotStarted;
            item.CreationDate = DateTime.Now;

            itemRepository.Add(item);
        }

        public async Task AddAsync(TodoListItem item)
        {
            await itemValidator.ValidateAsync(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Register);
            });

            item.UserId = CurrentUserId;
            item.Status ??= Status.NotStarted;
            item.CreationDate = DateTime.Now;

            await itemRepository.AddAsync(item);
        }

        public void Update(TodoListItem model)
        {
            var item = itemRepository.GetById(model.Id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), model.Id);

            itemValidator.Validate(item, opt =>
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

            itemRepository.Update(item);
        }

        public async Task UpdateAsync(TodoListItem model)
        {
            var item = await itemRepository.GetByIdAsync(model.Id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), model.Id);

            await itemValidator.ValidateAsync(item, opt =>
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

            await itemRepository.UpdateAsync(item);
        }

        public void Delete(Guid id)
        {
            var item = itemRepository.GetById(id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Delete);
            });

            itemRepository.Remove(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await itemRepository.GetByIdAsync(id, CurrentUserId)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            itemValidator.Validate(item, opt =>
            {
                opt.ThrowOnFailures();
                opt.IncludeRuleSets(RuleSetNames.Delete);
            });

            await itemRepository.RemoveAsync(item);
        }
    }
}
