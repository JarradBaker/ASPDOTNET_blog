using System.Net;
using DOTNET_DIARIES.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_DIARIES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // GET: api/images
        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageURL = await imageRepository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Image upload failed.", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new
            {
                link = imageURL
            });
        }
        
    }
}
