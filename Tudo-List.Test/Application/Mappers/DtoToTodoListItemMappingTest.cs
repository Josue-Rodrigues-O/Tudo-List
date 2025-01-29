using AutoMapper;
using Tudo_List.Application.Dtos.TodoListItem;
using Tudo_List.Application.Mappers;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Enums;

namespace Tudo_List.Test.Application.Mappers
{
    public class DtoToTodoListItemMappingTest : UnitTest
    {
        private readonly IMapper _mapper;

        public DtoToTodoListItemMappingTest()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new DtoToTodoListItemMapping());
            });

            _mapper = mappingConfig.CreateMapper();
        }

        [Fact]
        public void Should_Map_TodoListItemDto_To_TodoListItem_Ignoring_User_And_UserId_Properties()
        {
            var todoListItemDto = new TodoListItemDto
            (
                Id: Guid.NewGuid(),
                CreationDate: DateTime.Now,
                Description: "Description",
                Priority: Priority.Low,
                Status: Status.NotStarted,
                Title: "Title"
            );

            var todoListItem = _mapper.Map<TodoListItem>(todoListItemDto);

            Assert.Equal(todoListItemDto.Id, todoListItem.Id);
            Assert.Equal(todoListItemDto.CreationDate, todoListItem.CreationDate);
            Assert.Equal(todoListItemDto.Description, todoListItem.Description);
            Assert.Equal(todoListItemDto.Priority, todoListItem.Priority);
            Assert.Equal(todoListItemDto.Status, todoListItem.Status);
            Assert.Equal(todoListItemDto.Title, todoListItem.Title);

            Assert.Equal(default, todoListItem.User);
            Assert.Equal(default, todoListItem.UserId);
        }
        
        [Fact]
        public void Should_Map_TodoListItem_To_TodoListItemDto_Ignoring_User_And_UserId_Properties()
        {
            var todoListItem = new TodoListItem
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                Description = "Description",
                Priority = Priority.Low,
                Status = Status.NotStarted,
                Title = "Title",
                UserId = 5
            };

            var todoListItemDto = _mapper.Map<TodoListItemDto>(todoListItem);

            Assert.Equal(todoListItem.Id, todoListItemDto.Id);
            Assert.Equal(todoListItem.CreationDate, todoListItemDto.CreationDate);
            Assert.Equal(todoListItem.Description, todoListItemDto.Description);
            Assert.Equal(todoListItem.Priority, todoListItemDto.Priority);
            Assert.Equal(todoListItem.Status, todoListItemDto.Status);
            Assert.Equal(todoListItem.Title, todoListItemDto.Title);
        }
        
        [Fact]
        public void Should_Map_AddItemDto_To_TodoListItem_Ignoring_Id_User_UserId_And_CreationDate_Properties()
        {
            var addItemDto = new AddItemDto
            (
                Description: "Description",
                Priority: Priority.Low,
                Status: Status.NotStarted,
                Title: "Title"
            );

            var todoListItem = _mapper.Map<TodoListItem>(addItemDto);

            Assert.Equal(addItemDto.Description, todoListItem.Description);
            Assert.Equal(addItemDto.Priority, todoListItem.Priority);
            Assert.Equal(addItemDto.Status, todoListItem.Status);
            Assert.Equal(addItemDto.Title, todoListItem.Title);

            Assert.Equal(default, todoListItem.Id);
            Assert.Equal(default, todoListItem.User);
            Assert.Equal(default, todoListItem.UserId);
            Assert.Equal(default, todoListItem.CreationDate);
        }
        
        [Fact]
        public void Should_Map_UpdateItemDto_To_TodoListItem_Ignoring_User_UserId_And_CreationDate_Properties()
        {
            var updateItemDto = new UpdateItemDto
            (
                Id: Guid.NewGuid(),
                Description: "Description",
                Priority: Priority.Low,
                Status: Status.NotStarted,
                Title: "Title"
            );

            var todoListItem = _mapper.Map<TodoListItem>(updateItemDto);

            Assert.Equal(updateItemDto.Id, todoListItem.Id);
            Assert.Equal(updateItemDto.Description, todoListItem.Description);
            Assert.Equal(updateItemDto.Priority, todoListItem.Priority);
            Assert.Equal(updateItemDto.Status, todoListItem.Status);
            Assert.Equal(updateItemDto.Title, todoListItem.Title);

            Assert.Equal(default, todoListItem.User);
            Assert.Equal(default, todoListItem.UserId);
            Assert.Equal(default, todoListItem.CreationDate);
        }
    }
}
