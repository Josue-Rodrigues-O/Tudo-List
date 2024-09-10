using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Test.Infrastructure.Repositories
{
    public class TodoListItemRepositoryTest : UnitTest
    {
        private readonly ITodoListItemRepository _itemRepository;

        public TodoListItemRepositoryTest()
        {
            _itemRepository = _serviceProvider.GetRequiredService<ITodoListItemRepository>();

            _context.Users.Add(new User { Id = 5, Name = "Test" });
            InitializeInMemoryDatabase(GetItems());
        }

        private static IImmutableList<TodoListItem> GetItems()
        {
            return [
                new() { Id = Guid.NewGuid(), Title = "Do the dishes", CreationDate = DateTime.Now, Status = Status.Completed, Priority = Priority.Low, UserId = 5 },
                new() { Id = Guid.NewGuid(), Title = "Clean the house", CreationDate = DateTime.Now, Status = Status.Completed, Priority = Priority.Low, UserId = 5 },
                new() { Id = Guid.NewGuid(), Title = "Wash the car", CreationDate = DateTime.Now, Status = Status.Completed, Priority = Priority.Low, UserId = 5 },
            ];
        }
    }
}
