using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Domain.Models;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Domain.Services
{
    public class TodoListItemServiceTest : UnitTest
    {
        private readonly ITodoListItemService _itemService;
        private static User CurrentUser => MockData.GetCurrentUser();

        public TodoListItemServiceTest()
        {
            _itemService = _serviceProvider.GetRequiredService<ITodoListItemService>();

            _context.Users.Add(MockData.GetCurrentUser());
            SaveInMemoryDatabase(MockData.GetItems());
        }

        [Fact]
        public void Can_Get_All_TodoListItems_From_Current_User_Synchronously()
        {
            var todoListItems = MockData.GetItems().Where(item => item.UserId == CurrentUser.Id);
            var todoListItemsInDatabase = _itemService.GetAll(new TodoListItemQueryFilter(null, null, null, null, null, null));

            Assert.Equal(todoListItems.Count(), todoListItemsInDatabase.Count());
        }

        [Fact]
        public void Can_Get_A_TodoListItem_From_The_Current_User_Synchronously()
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

            var todoListItemInDatabase = _itemService.GetById(tudoListItem.Id);

            tudoListItem.User = null!;

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public void Can_Add_A_TodoListItem_With_Valid_Properties_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
            };

            _itemService.Add(tudoListItem);

            var todoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.NotNull(todoListItemInDatabase);
            Assert.Equal(CurrentUser.Id, todoListItemInDatabase.UserId);
            Assert.NotEqual(default, todoListItemInDatabase.CreationDate);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void Cant_Add_A_TodoListItem_With_Invalid_Properties_Synchronously(string? invalidTitle)
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = invalidTitle!,
            };

            Assert.Throws<ValidationException>(() => _itemService.Add(tudoListItem));
        }

        [Fact]
        public void Can_Update_A_TodoListItem_With_Valid_Properties_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            SaveInMemoryDatabase(tudoListItem);

            tudoListItem.Title = "Update test";
            tudoListItem.Description = "Update Test";
            tudoListItem.Status = Status.Completed;
            tudoListItem.Priority = Priority.Medium;


            _itemService.Update(tudoListItem);

            var tudoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public void Cant_Update_A_TodoListItem_If_User_Is_Not_Current_User_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            _context.Add(tudoListItem);

            tudoListItem.Title = "Update test";
            tudoListItem.Description = "Update Test";
            tudoListItem.Status = Status.Completed;
            tudoListItem.Priority = Priority.Medium;
            tudoListItem.UserId = 5;

            Assert.Throws<EntityNotFoundException>(() => _itemService.Update(tudoListItem));
        }

        [Fact]
        public void Cant_Update_A_Non_Existent_TodoListItem_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            Assert.Throws<EntityNotFoundException>(() => _itemService.Update(tudoListItem));
        }

        [Fact]
        public void Can_Delete_A_TodoListItem_Synchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id,
            };

            SaveInMemoryDatabase(tudoListItem);

            _itemService.Delete(tudoListItem.Id);

            var tudoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.Null(tudoListItemInDatabase);
        }

        [Fact]
        public void Cant_Remove_A_Non_Existent_TodoListItem_Synchronously()
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

            Assert.Throws<EntityNotFoundException>(() => _itemService.Delete(tudoListItem.Id));
        }

        [Fact]
        public void Cant_Remove_A_TodoListItem_If_It_Is_Not_From_The_User_Synchronously()
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

            Assert.Throws<EntityNotFoundException>(() => _itemService.Delete(tudoListItem.Id));
        }

        [Fact]
        public async Task Can_Get_All_TodoListItems_From_Current_User_Asynchronously()
        {
            var todoListItems = MockData.GetItems().Where(item => item.UserId == CurrentUser.Id);
            var todoListItemsInDatabase = await _itemService.GetAllAsync(new TodoListItemQueryFilter(null, null, null, null, null, null));

            Assert.Equal(todoListItems.Count(), todoListItemsInDatabase.Count());
        }

        [Fact]
        public async Task Can_Get_A_TodoListItem_From_The_Current_User_Asynchronously()
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

            var todoListItemInDatabase = await _itemService.GetByIdAsync(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, todoListItemInDatabase, true);
        }

        [Fact]
        public async Task Can_Add_A_TodoListItem_With_Valid_Properties_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Title = "Test",
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
            };

            await _itemService.AddAsync(tudoListItem);

            var todoListItemInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.NotNull(todoListItemInDatabase);
            Assert.Equal(CurrentUser.Id, todoListItemInDatabase.UserId);
            Assert.NotEqual(default, todoListItemInDatabase.CreationDate);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public async Task Cant_Add_A_TodoListItem_With_Invalid_Properties_Asynchronously(string? invalidTitle)
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = invalidTitle!,
            };

            await Assert.ThrowsAsync<ValidationException>(() => _itemService.AddAsync(tudoListItem));
        }

        [Fact]
        public async Task Can_Update_A_TodoListItem_With_Valid_Properties_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            SaveInMemoryDatabase(tudoListItem);

            tudoListItem.Title = "Update test";
            tudoListItem.Description = "Update Test";
            tudoListItem.Status = Status.Completed;
            tudoListItem.Priority = Priority.Medium;

            await _itemService.UpdateAsync(tudoListItem);

            var tudoListItemInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.Equivalent(tudoListItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public async Task Cant_Update_A_TodoListItem_If_User_Is_Not_Current_User_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            await _context.AddAsync(tudoListItem);

            tudoListItem.Title = "Update test";
            tudoListItem.Description = "Update Test";
            tudoListItem.Status = Status.Completed;
            tudoListItem.Priority = Priority.Medium;
            tudoListItem.UserId = 5;

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _itemService.UpdateAsync(tudoListItem));
        }

        [Fact]
        public async Task Cant_Update_A_Non_Existent_TodoListItem_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _itemService.UpdateAsync(tudoListItem));
        }

        [Fact]
        public async Task Can_Delete_A_TodoListItem_Asynchronously()
        {
            var tudoListItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id,
            };

            SaveInMemoryDatabase(tudoListItem);

            await _itemService.DeleteAsync(tudoListItem.Id);

            var tudoListItemInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.Null(tudoListItemInDatabase);
        }

        [Fact]
        public async Task Cant_Remove_A_Non_Existent_TodoListItem_Asynchronously()
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

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _itemService.DeleteAsync(tudoListItem.Id));
        }

        [Fact]
        public async Task Cant_Remove_A_TodoListItem_If_It_Is_Not_From_The_User_Asynchronously()
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

            await Assert.ThrowsAsync<EntityNotFoundException>(() => _itemService.DeleteAsync(tudoListItem.Id));
        }
    }
}
