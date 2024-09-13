using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;
using Tudo_list.Infrastructure.Repositories;
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

            _context.Users.Add(new User { Id = 5, Name = "Test", Email = "test@test.com", PasswordHash = "12356789" });
            InitializeInMemoryDatabase(GetItems());
        }


        [Fact]
        public void Can_Get_All_TodoListItems_Synchronously()
        {
            var todoListItems = GetItems();
            var todoListItemsInDatabase = _itemRepository.GetAll();

            Assert.Equivalent(todoListItems.Count, todoListItemsInDatabase.Count());
        }

        [Fact]
        public void Can_Get_An_TodoListItem_By_Id_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            _itemRepository.Add(tudoListItem);

            var todoListItemInDatabase = _itemRepository.GetById(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public void Can_Add_An_TodoListItem_With_Valid_Properties_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            _itemRepository.Add(tudoListItem);

            var todoListItemInDatabase = _context.TodoListItems.FirstOrDefault(x => x.Id == tudoListItem.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public void Cant_Add_An_TodoListItem_With_Invalid_Property_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = null,
                UserId = 5
            };

            Assert.Throws<DbUpdateException>(() => _itemRepository.Add(tudoListItem));
        }

        [Fact]
        public void Can_Update_An_TodoListItem_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            _context.Add(tudoListItem);
            _context.SaveChanges();

            tudoListItem.Title = "Update test";

            _itemRepository.Update(tudoListItem);

            var tudoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public void Can_Remove_An_TodoListItem_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            _context.Add(tudoListItem);
            _context.SaveChanges();

            _itemRepository.Remove(tudoListItem);

            var tudoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.Null(tudoListItemInDatabase);
        }

        [Fact]
        public void Cant_Remove_An_Inexisting_TodoListItem_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _itemRepository.Remove(tudoListItem));
        }

        [Fact]
        public async Task Can_Get_All_TodoListItems_Asynchronously()
        {
            var tudoListItems = GetItems();

            var tudoListItemsInDatabase = await _itemRepository.GetAllAsync();

            Assert.Equivalent(tudoListItems.Count(), tudoListItemsInDatabase.Count());
        }

        [Fact]
        public async Task Can_Get_An_TodoListItem_By_Id_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            await _itemRepository.AddAsync(tudoListItem);

            var todoListItemInDatabase = await _itemRepository.GetByIdAsync(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public async Task Can_Add_An_TodoListItem_With_Valid_Properties_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            await _itemRepository.AddAsync(tudoListItem);

            var itemInDatabase = await _context.TodoListItems.FirstOrDefaultAsync(x => x.Id == tudoListItem.Id);

            Assert.Equivalent(tudoListItem, itemInDatabase, true);
        }

        [Fact]
        public async Task Cant_Add_An_TodoListItem_With_Invalid_Properties_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = null,
                UserId = 5
            };

            await Assert.ThrowsAsync<DbUpdateException>(async () => await _itemRepository.AddAsync(tudoListItem));
        }

        [Fact]
        public async Task Can_Update_An_TodoListItem_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            await _context.AddAsync(tudoListItem);
            await _context.SaveChangesAsync();

            tudoListItem.Title = "Update test";

            await _itemRepository.UpdateAsync(tudoListItem);

            var tudoListItemInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public async Task Can_Remove_An_TodoListItem_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            await _context.AddAsync(tudoListItem);
            await _context.SaveChangesAsync();

            await _itemRepository.RemoveAsync(tudoListItem);

            var userInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.Null(userInDatabase);
        }

        [Fact]
        public async Task Cant_Remove_An_Inexisting_TodoListItem_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = 5
            };

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _itemRepository.RemoveAsync(tudoListItem));
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
