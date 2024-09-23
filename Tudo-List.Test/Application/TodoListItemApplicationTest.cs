using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tudo_List.Application.Dtos.TodoListItem;
using Tudo_List.Application.Interfaces.Applications;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;
using Tudo_List.Domain.Exceptions;
using Tudo_List.Test.Mock;

namespace Tudo_List.Test.Application
{
    public class TodoListItemApplicationTest : UnitTest
    {
        private readonly ITodoListItemApplication _todoListItemApplication;
        private static User CurrentUser => MockData.GetCurrentUser();

        public TodoListItemApplicationTest()
        {
            _todoListItemApplication = _serviceProvider.GetRequiredService<ITodoListItemApplication>();

            _context.Users.Add(MockData.GetCurrentUser());
            InitializeInMemoryDatabase(MockData.GetItems());
        }

        [Fact]
        public void Can_Get_All_TodoListItems_From_Current_User_Synchronously()
        {
            var todoListItems = MockData.GetItems().Where(item => item.UserId == CurrentUser.Id);
            var todoListItemsInDatabase = _todoListItemApplication.GetAll();

            Assert.Equal(todoListItems.Count(), todoListItemsInDatabase.Count());

            for (int i = 0; i < todoListItemsInDatabase.Count(); i++)
            {
                var todoListItem = todoListItems.ElementAt(i);
                var todoListItemInDatabase = todoListItemsInDatabase.ElementAt(i);

                Assert.Equal(todoListItem.Title, todoListItemInDatabase.Title);
                Assert.Equal(todoListItem.Description, todoListItemInDatabase.Description);
                Assert.Equal(todoListItem.Priority, todoListItemInDatabase.Priority);
                Assert.Equal(todoListItem.Status, todoListItemInDatabase.Status);
            }
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

            var expectedItem = new TodoListItemDto()
            {
                Id = tudoListItem.Id,
                CreationDate = tudoListItem.CreationDate,
                Description = tudoListItem.Description,
                Priority = (Priority)tudoListItem.Priority,
                Status = (Status)tudoListItem.Status,
                Title = tudoListItem.Title,
            };
            _context.Add(tudoListItem);
            _context.SaveChanges();

            var todoListItemInDatabase = _todoListItemApplication.GetById(tudoListItem.Id);

            tudoListItem.User = null;

            Assert.Equivalent(expectedItem, todoListItemInDatabase, true);
        }

        [Fact]
        public void It_Is_Not_Possible_To_Obtain_An_Item_That_Does_Not_Exist_In_The_Bank_Synchronously()
        {
            Assert.Throws<EntityNotFoundException>(() => _todoListItemApplication.GetById(Guid.NewGuid()));
        }

        [Fact]
        public void Can_Add_A_TodoListItem_With_Valid_Properties_Synchronously()
        {
            var tudoListItem = new AddItemDto()
            {
                Title = "Test add item dto 1",
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
            };

            _todoListItemApplication.Add(tudoListItem);

            var todoListItemInDatabase = _context.TodoListItems.FirstOrDefault(x => x.Title == tudoListItem.Title);

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
            var tudoListItem = new AddItemDto()
            {
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = invalidTitle,
            };

            Assert.Throws<ValidationException>(() => _todoListItemApplication.Add(tudoListItem));
        }

        [Fact]
        public void Can_Update_A_TodoListItem_With_Valid_Properties_Synchronously()
        {
            var newItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            var tudoListItem = new UpdateItemDto()
            {
                ItemId = newItem.Id,
                Description = "Update test",
                Priority = Priority.Medium,
                Status = Status.Completed,
                Title = "Update test",
            };

            _context.Add(newItem);
            _context.SaveChanges();

            _todoListItemApplication.Update(tudoListItem);

            var tudoListItemInDatabase = _context.TodoListItems.Find(newItem.Id);

            Assert.Equivalent(newItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public void Cant_Update_A_TodoListItem_If_User_Is_Not_Current_User_Synchronously()
        {
            var newItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            var tudoListItem = new UpdateItemDto()
            {
                ItemId = newItem.Id,
                Description = "Update test",
                Priority = Priority.Medium,
                Status = Status.Completed,
                Title = "Update test",
            };
            _context.Add(newItem);

            Assert.Throws<EntityNotFoundException>(() => _todoListItemApplication.Update(tudoListItem));
        }

        [Fact]
        public void Cant_Update_A_Non_Existent_TodoListItem_Synchronously()
        {
            var tudoListItem = new UpdateItemDto()
            {
                ItemId = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
            };

            Assert.Throws<EntityNotFoundException>(() => _todoListItemApplication.Update(tudoListItem));
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

            _context.Add(tudoListItem);
            _context.SaveChanges();

            _todoListItemApplication.Delete(tudoListItem.Id);

            var tudoListItemInDatabase = _context.TodoListItems.Find(tudoListItem.Id);

            Assert.Null(tudoListItemInDatabase);
        }

        [Fact]
        public void Cant_Delete_A_Non_Existent_TodoListItem_Synchronously()
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

            Assert.Throws<EntityNotFoundException>(() => _todoListItemApplication.Delete(tudoListItem.Id));
        }

        [Fact]
        public void Cant_Delete_A_TodoListItem_If_It_Is_Not_From_The_User_Synchronously()
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

            Assert.Throws<EntityNotFoundException>(() => _todoListItemApplication.Delete(tudoListItem.Id));
        }

        [Fact]
        public async Task Can_Get_All_TodoListItems_From_Current_User_Asynchronously()
        {
            var todoListItems = MockData.GetItems().Where(item => item.UserId == CurrentUser.Id);
            var todoListItemsInDatabase = await _todoListItemApplication.GetAllAsync();

            Assert.Equal(todoListItems.Count(), todoListItemsInDatabase.Count());

            for (int i = 0; i < todoListItemsInDatabase.Count(); i++)
            {
                var todoListItem = todoListItems.ElementAt(i);
                var todoListItemInDatabase = todoListItemsInDatabase.ElementAt(i);

                Assert.Equal(todoListItem.Title, todoListItemInDatabase.Title);
                Assert.Equal(todoListItem.Description, todoListItemInDatabase.Description);
                Assert.Equal(todoListItem.Priority, todoListItemInDatabase.Priority);
                Assert.Equal(todoListItem.Status, todoListItemInDatabase.Status);
            }
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

            var expectedItem = new TodoListItemDto()
            {
                Id = tudoListItem.Id,
                CreationDate = tudoListItem.CreationDate,
                Description = tudoListItem.Description,
                Priority = (Priority)tudoListItem.Priority,
                Status = (Status)tudoListItem.Status,
                Title = tudoListItem.Title,
            };

            await _context.AddAsync(tudoListItem);
            await _context.SaveChangesAsync();

            var todoListItemInDatabase = await _todoListItemApplication.GetByIdAsync(tudoListItem.Id);

            Assert.Equivalent(expectedItem, todoListItemInDatabase, true);
        }

        [Fact]
        public async Task It_Is_Not_Possible_To_Obtain_An_Item_That_Does_Not_Exist_In_The_Bank_Asynchronously()
        {
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _todoListItemApplication.GetByIdAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task Can_Add_A_TodoListItem_With_Valid_Properties_Asynchronously()
        {
            var tudoListItem = new AddItemDto()
            {
                Title = "Test add item",
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
            };

            await _todoListItemApplication.AddAsync(tudoListItem);

            var todoListItemInDatabase = _context.TodoListItems.FirstOrDefault(x => x.Title == tudoListItem.Title);

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
            var tudoListItem = new AddItemDto()
            {
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = invalidTitle,
            };

            await Assert.ThrowsAsync<ValidationException>(async () => await _todoListItemApplication.AddAsync(tudoListItem));
        }

        [Fact]
        public async Task Can_Update_A_TodoListItem_With_Valid_Properties_Asynchronously()
        {
            var newItem = new TodoListItem()
            {
                Id = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
                UserId = CurrentUser.Id
            };

            var tudoListItem = new UpdateItemDto()
            {
                ItemId = newItem.Id,
                Description = "Update test",
                Priority = Priority.Medium,
                Status = Status.Completed,
                Title = "Update test",
            };

            await _context.AddAsync(newItem);
            await _context.SaveChangesAsync();

            await _todoListItemApplication.UpdateAsync(tudoListItem);

            var tudoListItemInDatabase = await _context.TodoListItems.FindAsync(newItem.Id);

            Assert.Equivalent(newItem, tudoListItemInDatabase, true);
        }

        [Fact]
        public async Task Cant_Update_A_TodoListItem_If_User_Is_Not_Current_User_Asynchronously()
        {
            var tudoListItem = new UpdateItemDto()
            {
                ItemId = Guid.NewGuid(),
                Description = "Update test",
                Priority = Priority.Medium,
                Status = Status.Completed,
                Title = "Update test",
            };

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _todoListItemApplication.UpdateAsync(tudoListItem));
        }

        [Fact]
        public async Task Cant_Update_A_Non_Existent_TodoListItem_Asynchronously()
        {
            var tudoListItem = new UpdateItemDto()
            {
                ItemId = Guid.NewGuid(),
                Description = "Test",
                Priority = Priority.Medium,
                Status = Status.NotStarted,
                Title = "Test",
            };

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _todoListItemApplication.UpdateAsync(tudoListItem));
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

            await _context.AddAsync(tudoListItem);
            await _context.SaveChangesAsync();

            await _todoListItemApplication.DeleteAsync(tudoListItem.Id);

            var tudoListItemInDatabase = await _context.TodoListItems.FindAsync(tudoListItem.Id);

            Assert.Null(tudoListItemInDatabase);
        }

        [Fact]
        public async Task Cant_Delete_A_Non_Existent_TodoListItem_Asynchronously()
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

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _todoListItemApplication.DeleteAsync(tudoListItem.Id));
        }

        [Fact]
        public async Task Cant_Delete_A_TodoListItem_If_It_Is_Not_From_The_User_Asynchronously()
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

            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _todoListItemApplication.DeleteAsync(tudoListItem.Id));
        }
    }
}
