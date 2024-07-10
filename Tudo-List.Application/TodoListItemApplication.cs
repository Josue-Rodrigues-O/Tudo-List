using AutoMapper;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos.TodoListItem;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application
{
    public class TodoListItemApplication(ITodoListItemService todoListItemService, IMapper mapper, ICurrentUserService currentUserService) : ITodoListItemApplication
    {
        private readonly ITodoListItemService _todoListItemService = todoListItemService;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;

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

        public void Add(AddItemDto model)
        {
            var item = _mapper.Map<TodoListItem>(model);
            item.UserId = GetCurrentUserId();
            _todoListItemService.Add(item);
        }

        public async Task AddAsync(AddItemDto model)
        {
            var item = _mapper.Map<TodoListItem>(model);
            item.UserId = GetCurrentUserId();
            await _todoListItemService.AddAsync(item);
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

        private int GetCurrentUserId()
        {
            var strUserId = _currentUserService.Id;

            if (!int.TryParse(strUserId, out int userId))
                throw new Exception();

            return userId;
        }
    }
}
