using AutoMapper;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos.TodoListItem;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Exceptions;

namespace Tudo_List.Application
{
    public class TodoListItemApplication(ITodoListItemService todoListItemService, IMapper mapper, ICurrentUserService currentUserService) : ITodoListItemApplication
    {
        private readonly ITodoListItemService _todoListItemService = todoListItemService;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        private int CurrentUserId => int.Parse(_currentUserService.Id);

        public IEnumerable<TodoListItemDto> GetAll()
        {
            return _mapper.Map<IEnumerable<TodoListItemDto>>(_todoListItemService.GetAll());
        }

        public async Task<IEnumerable<TodoListItemDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TodoListItemDto>>(await _todoListItemService.GetAllAsync());
        }

        public TodoListItemDto GetById(Guid id)
        {
            var item = _todoListItemService.GetById(id)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            return _mapper.Map<TodoListItemDto>(item);
        }

        public async Task<TodoListItemDto> GetByIdAsync(Guid id)
        {
            var item = await _todoListItemService.GetByIdAsync(id)
                ?? throw new EntityNotFoundException(nameof(TodoListItem), nameof(TodoListItem.Id), id);

            return _mapper.Map<TodoListItemDto>(item);
        }

        public void Add(AddItemDto model)
        {
            var item = _mapper.Map<TodoListItem>(model);
            _todoListItemService.Add(item, CurrentUserId);
        }

        public async Task AddAsync(AddItemDto model)
        {
            var item = _mapper.Map<TodoListItem>(model);
            await _todoListItemService.AddAsync(item, CurrentUserId);
        }

        public void Update(UpdateItemDto model)
        {
            _todoListItemService.Update(_mapper.Map<TodoListItem>(model));
        }

        public async Task UpdateAsync(UpdateItemDto model)
        {
            await _todoListItemService.UpdateAsync(_mapper.Map<TodoListItem>(model));
        }

        public void Delete(Guid id)
        {
            _todoListItemService.Delete(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _todoListItemService.DeleteAsync(id);
        }
    }
}
