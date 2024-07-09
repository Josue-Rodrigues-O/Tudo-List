using AutoMapper;
using System.Reflection;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Application.Interfaces.Services;
using Tudo_List.Application.Models.Dtos;
using Tudo_List.Application.Services;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models.TodoListItem;

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

        public void Add(AddItemRequest model)
        {
            var item = _mapper.Map<TodoListItem>(model);
            item.UserId = GetCurrentUserId();
            _todoListItemService.Add(item);
        }

        public async Task AddAsync(AddItemRequest model)
        {
            var item = _mapper.Map<TodoListItem>(model);
            item.UserId = GetCurrentUserId();
            await _todoListItemService.AddAsync(item);
        }

        public void Update(UpdateItemRequest model)
        {
            _todoListItemService.Update(model);
        }

        public async Task UpdateAsync(UpdateItemRequest model)
        {
            await _todoListItemService.UpdateAsync(model);
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
