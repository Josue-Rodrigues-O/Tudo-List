using Microsoft.AspNetCore.Mvc;
using Tudo_List.Domain.Core.Interfaces.Services;

namespace Tudo_List.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
    }
}
