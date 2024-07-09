using Tudo_List.Application.Models.Dtos;
using Tudo_List.Domain.Models.User;

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

        void Register(RegisterUserRequest model);
        Task RegisterAsync(RegisterUserRequest model);

        void Update(UpdateUserRequest model);
        Task UpdateAsync(UpdateUserRequest model);

        public void UpdateEmail(UpdateEmailRequest model);
        public Task UpdateEmailAsync(UpdateEmailRequest model);

        public void UpdatePassword(UpdatePasswordRequest model);
        public Task UpdatePasswordAsync(UpdatePasswordRequest model);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
