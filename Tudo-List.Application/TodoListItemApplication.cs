using AutoMapper;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Models.Dtos.TodoListItem;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application
{
    public class TodoListItemApplication(ITodoListItemService todoListItemService, IMapper mapper) : ITodoListItemApplication
    {
        private readonly ITodoListItemService _todoListItemService = todoListItemService;
        private readonly IMapper _mapper = mapper;

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
            return _mapper.Map<TodoListItemDto>(_todoListItemService.GetById(id));
        }

        public async Task<TodoListItemDto> GetByIdAsync(Guid id)
        {
            return _mapper.Map<TodoListItemDto>(await _todoListItemService.GetByIdAsync(id));
        }

        public void Add(AddItemDto item)
        {
            _todoListItemService.Add(_mapper.Map<TodoListItem>(item));
        }

        public async Task AddAsync(AddItemDto item)
        {
            await _todoListItemService.AddAsync(_mapper.Map<TodoListItem>(item));
        }

        public void Update(UpdateItemDto item)
        {
            _todoListItemService.Update(_mapper.Map<TodoListItem>(item));
        }

        public async Task UpdateAsync(UpdateItemDto item)
        {
            await _todoListItemService.UpdateAsync(_mapper.Map<TodoListItem>(item));
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
