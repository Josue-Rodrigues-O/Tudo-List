using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Repositories;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Models;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Infrastructure.Repositories
{
    public class TodoListItemRepositoryTest : UnitTest
    {
        private readonly ITodoListItemRepository _itemRepository;
        private static User CurrentUser => MockData.GetCurrentUser();

        public TodoListItemRepositoryTest()
        {
            _itemRepository = _serviceProvider.GetRequiredService<ITodoListItemRepository>();

            _context.Users.Add(MockData.GetCurrentUser());
            SaveInMemoryDatabase(MockData.GetItems());
        }
        
        [Fact]
        public void Can_Get_All_TodoListItems_From_An_User_Synchronously()
        {
            var todoListItems = MockData.GetItems().Where(item => item.UserId == CurrentUser.Id);
            var todoListItemsInDatabase = _itemRepository.GetAll(CurrentUser.Id, new TodoListItemQueryFilter(null, null, null, null, null, null));

            Assert.Equal(todoListItems.Count(), todoListItemsInDatabase.Count());
        }

        [Fact]
        public void Can_Get_Any_TodoListItem_By_Id_Synchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            var todoListItemInDatabase = _itemRepository.GetById(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public void Should_Return_Null_When_Trying_To_Get_an_Inexistent_User_By_Id_Synchronously()
        {
            var item = _itemRepository.GetById(Guid.NewGuid());

            Assert.Null(item);
        }

        [Fact]
        public void Can_Get_A_TodoListItem_By_Id_If_It_Is_From_The_Current_User_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            SaveInMemoryDatabase(tudoListItem);

            tudoListItem.User = null!;

            var todoListItemInDatabase = _itemRepository.GetById(tudoListItem.Id, CurrentUser.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public void Cant_Get_A_TodoListItem_By_Id_If_It_Is_Not_From_The_Current_User_Synchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            var todoListItemInDatabase = _itemRepository.GetById(tudoListItem.Id, CurrentUser.Id);

            Assert.Null(todoListItemInDatabase);
        }

        [Fact]
        public void Can_Add_A_TodoListItem_With_Valid_Properties_Synchronously()
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
        public void Cant_Add_A_TodoListItem_With_Invalid_Property_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = null!,
                UserId = 5
            };

            Assert.Throws<DbUpdateException>(() => _itemRepository.Add(tudoListItem));
        }

        [Fact]
        public void Can_Update_A_TodoListItem_Synchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            tudoListItem.Title = "Update test";

            _itemRepository.Update(tudoListItem);

            var tudoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public void Can_Remove_A_TodoListItem_Synchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            _itemRepository.Remove(tudoListItem);

            var tudoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.Null(tudoListItemInDatabase);
        }

        [Fact]
        public void Cant_Remove_An_Non_Existent_TodoListItem_Synchronously()
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
        public async Task Can_Get_All_TodoListItems_From_An_User_Asynchronously()
        {
            var todoListItems = MockData.GetItems().Where(item => item.UserId == CurrentUser.Id);
            var todoListItemsInDatabase = await _itemRepository.GetAllAsync(CurrentUser.Id, new TodoListItemQueryFilter(null, null, null, null, null, null));

            Assert.Equal(todoListItems.Count(), todoListItemsInDatabase.Count());
        }

        [Fact]
        public async Task Can_Get_Any_TodoListItem_By_Id_Asynchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            var todoListItemInDatabase = await _itemRepository.GetByIdAsync(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public async Task Should_Return_Null_When_Trying_To_Get_an_Inexistent_User_By_Id_Asynchronously()
        {
            var item = await _itemRepository.GetByIdAsync(Guid.NewGuid());

            Assert.Null(item);
        }

        [Fact]
        public async Task Can_Get_A_TodoListItem_By_Id_If_It_Is_From_The_Current_User_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            SaveInMemoryDatabase(tudoListItem);

            tudoListItem.User = null!;

            var todoListItemInDatabase = await _itemRepository.GetByIdAsync(tudoListItem.Id, CurrentUser.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public async Task Cant_Get_A_TodoListItem_By_Id_If_It_Is_Not_From_The_Current_User_Asynchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            var todoListItemInDatabase = await _itemRepository.GetByIdAsync(tudoListItem.Id, CurrentUser.Id);

            Assert.Null(todoListItemInDatabase);
        }

        [Fact]
        public async Task Can_Add_A_TodoListItem_With_Valid_Properties_Asynchronously()
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
        public async Task Cant_Add_A_TodoListItem_With_Invalid_Properties_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = null!,
                UserId = 5
            };

            await Assert.ThrowsAsync<DbUpdateException>(() => _itemRepository.AddAsync(tudoListItem));
        }

        [Fact]
        public async Task Can_Update_A_TodoListItem_Asynchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            tudoListItem.Title = "Update test";

            await _itemRepository.UpdateAsync(tudoListItem);

            var tudoListItemInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public async Task Can_Remove_A_TodoListItem_Asynchronously()
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

            SaveInMemoryDatabase(tudoListItem);

            await _itemRepository.RemoveAsync(tudoListItem);

            var userInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.Null(userInDatabase);
        }

        [Fact]
        public async Task Cant_Remove_An_Non_Existent_TodoListItem_Asynchronously()
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

            await Assert.ThrowsAsync<DbUpdateConcurrencyException>(() => _itemRepository.RemoveAsync(tudoListItem));
        }
    }
}
