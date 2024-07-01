using AutoMapper;
using Tudo_List.Application.Interfaces;
using Tudo_List.Application.Models.Users;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application
{
    public class UserApplication(IUserService userService, IMapper mapper) : IUserApplication
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        public IEnumerable<User> GetAll()
        {
            return _userService.GetAll();
        }

        public User GetById(int id)
        {
            return _userService.GetById(id);
        }

        public void Register(RegisterUserRequest model)
        {
            var user = _mapper.Map<User>(model);
            _userService.Register(user);
        }

        public void Update(UpdateUserRequest model)
        {
            var user = _mapper.Map<User>(model);
            _userService.Update(user);
        }

        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }
}
