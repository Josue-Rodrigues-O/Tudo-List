using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Application.Interfaces.Applications;

namespace Tudo_List.Server.Controllers.V1
{
    [Authorize]
    [Route("api/user-images")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserImagesController(IUserImageApplication userImageApplication) : ControllerBase
    {
        [HttpPost("upload/{userId:int}")]
        public IActionResult Upload([FromRoute] int userId, [FromForm] IFormFile file)
        {
            var image = userImageApplication.Upload(userId, file);
            return File(image.Data, image.ContentType);
        }
        
        [HttpPost("upload-async/{userId:int}")]
        public async Task<IActionResult> UploadAsync([FromRoute] int userId, [FromForm] IFormFile file)
        {
            var image = await userImageApplication.UploadAsync(userId, file);
            return File(image.Data, image.ContentType);
        }
    }
}
