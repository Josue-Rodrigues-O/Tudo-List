using Tudo_List.Application.Models.Dtos;

namespace Tudo_List.Application.Interfaces.Applications
{
    public interface IUserApplication
    {
        IEnumerable<UserDto> GetAll();
        Task<IEnumerable<UserDto>> GetAllAsync();

        UserDto GetById(int id);
        Task<UserDto> GetByIdAsync(int id);

        UserDto GetByEmail(string email);
        Task<UserDto> GetByEmailAsync(string email);

        void Register(RegisterUserDto model);
        Task RegisterAsync(RegisterUserDto model);

        void Update(UpdateUserDto model);
        Task UpdateAsync(UpdateUserDto model);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
