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
        [HttpGet("get-by-user-id/{userId:int}")]
        public IActionResult GetByUserId(int userId)
        {
            var image = userImageApplication.GetByUserId(userId);
            
            return image == null
                ? NotFound()
                : File(image.Data, image.ContentType);
        }

        [HttpGet("get-by-user-id-async/{userId:int}")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            var image = await userImageApplication.GetByUserIdAsync(userId);

            return image == null
                ? NotFound()
                : File(image.Data, image.ContentType);
        }

        [HttpPost("upload/{userId:int}")]
        public FileContentResult Upload([FromRoute] int userId, [FromForm] IFormFile file)
        {
            var image = userImageApplication.Upload(userId, file);
            return File(image.Data, image.ContentType);
        }
        
        [HttpPost("upload-async/{userId:int}")]
        public async Task<FileContentResult> UploadAsync([FromRoute] int userId, [FromForm] IFormFile file)
        {
            var image = await userImageApplication.UploadAsync(userId, file);
            return File(image.Data, image.ContentType);
        }
    }
}
