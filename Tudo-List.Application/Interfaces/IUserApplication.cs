using Tudo_List.Application.Models.Dtos;
using Tudo_List.Application.Models.Requests;

namespace Tudo_List.Application.Interfaces
{
    public interface IUserApplication
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        void Register(RegisterUserRequest model);
        void Update(UpdateUserRequest model);
        void Delete(int id);
    }
}
