using AutoMapper;
using Tudo_List.Application.Interfaces;
using Tudo_List.Application.Models.Dtos;
using Tudo_List.Application.Models.Requests;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Entities;

namespace Tudo_List.Application
{
    public class UserApplication(IUserService userService, IMapper mapper) : IUserApplication
    {
        private readonly IUserService _userService = userService;
        private readonly IMapper _mapper = mapper;

        public IEnumerable<UserDto> GetAll()
        {
            return _mapper.Map<IEnumerable<UserDto>>(_userService.GetAll());
        }

        public UserDto GetById(int id)
        {
            return _mapper.Map<UserDto>(_userService.GetById(id));
        }

        public void Register(RegisterUserRequest model)
        {
            _userService.Register(_mapper.Map<User>(model));
        }

        public void Update(UpdateUserRequest model)
        {
            _userService.Update(_mapper.Map<User>(model));
        }

        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }
}
