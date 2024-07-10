using AutoMapper;
using Tudo_List.Application.Models.Dtos.User;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application.Mappers
{
    public class DtoToUserMapping : Profile
    {
        public DtoToUserMapping()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordStrategy, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterUserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordStrategy, opt => opt.Ignore());
        }
    }
}
