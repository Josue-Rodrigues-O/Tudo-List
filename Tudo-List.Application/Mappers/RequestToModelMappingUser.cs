using AutoMapper;
using Tudo_List.Application.Models.Users;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application.Mappers
{
    public class RequestToModelMappingUser : Profile
    {
        public RequestToModelMappingUser()
        {
            CreateMap<RegisterUserRequest, User>();

            CreateMap<UpdateUserRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        var isInvalidString = (prop.GetType() == typeof(string) && string.IsNullOrWhiteSpace((string)prop));

                        if (prop is null)
                            return false;

                        if (isInvalidString)
                            return false;

                        return true;
                    }));
        }
    }
}
