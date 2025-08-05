using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tudo_List.Domain.Core.Interfaces.Services;
using Tudo_List.Domain.Services.Helpers;

namespace Tudo_List.Application.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly ClaimsPrincipal _currentUserClaims = httpContextAccessor.HttpContext!.User;

        public string Id => _currentUserClaims.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new ArgumentNullException(nameof(Id), ValidationMessageHelper.GetNotFoundUserPropertyInJwtClaims(nameof(Id)));

        public string Name => _currentUserClaims.FindFirstValue(ClaimTypes.Name)
            ?? throw new ArgumentNullException(nameof(Name), ValidationMessageHelper.GetNotFoundUserPropertyInJwtClaims(nameof(Name)));
        
        public string Email => _currentUserClaims.FindFirstValue(ClaimTypes.Email)
            ?? throw new ArgumentNullException(nameof(Email), ValidationMessageHelper.GetNotFoundUserPropertyInJwtClaims(nameof(Email)));
    }
}
