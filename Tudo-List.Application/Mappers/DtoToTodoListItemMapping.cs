using AutoMapper;
using Tudo_List.Application.Dtos.TodoListItem;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application.Mappers
{
    public class DtoToTodoListItemMapping : Profile
    {
        public DtoToTodoListItemMapping()
        {
            {
                CreateMap<TodoListItemDto, TodoListItem>()
                    .ForMember(dest => dest.UserId, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                    .ReverseMap();

                CreateMap<AddItemDto, TodoListItem>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.UserId, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                    .ForMember(dest => dest.CreationDate, opt => opt.Ignore());

                CreateMap<UpdateItemDto, TodoListItem>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.UserId, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore())
                    .ForMember(dest => dest.CreationDate, opt => opt.Ignore());
            }
        }
    }
}
