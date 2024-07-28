using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tudo_List.Application.Interfaces.Services;

namespace Tudo_List.Application.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly ClaimsPrincipal? _currentUserClaims = httpContextAccessor.HttpContext?.User;

        public string Id => _currentUserClaims?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ArgumentNullException(nameof(Id), "The User Id was not found in JWT Claims!");

        public string Name => _currentUserClaims?.FindFirstValue(ClaimTypes.Name)
            ?? throw new ArgumentNullException(nameof(Name), "The User Name was not found in JWT Claims!");
        
        public string Email => _currentUserClaims?.FindFirstValue(ClaimTypes.Email)
            ?? throw new ArgumentNullException(nameof(Email), "The User Email was not found in JWT Claims!");
    }
}
