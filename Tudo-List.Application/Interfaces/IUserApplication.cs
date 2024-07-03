using Tudo_List.Application.Models.Dtos;

namespace Tudo_List.Application.Interfaces
{
    public interface IUserApplication
    {
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        void Register(RegisterUserDto model);
        void Update(UpdateUserDto model);
        void Delete(int id);
    }
}
