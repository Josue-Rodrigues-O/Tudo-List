using AutoMapper;
using Tudo_List.Application.Models.Dtos;
using Tudo_List.Domain.Entities;
using Tudo_List.Domain.Models.TodoListItem;

namespace Tudo_List.Application.Mappers
{
    public class DtoToTodoListItemMapping : Profile
    {
        public DtoToTodoListItemMapping()
        {
            {
                CreateMap<TodoListItemDto, TodoListItem>()
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                    .ReverseMap();

                CreateMap<AddItemRequest, TodoListItem>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.UserId, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                    .ForMember(dest => dest.CreationDate, opt => opt.Ignore());
            }
        }
    }
}
