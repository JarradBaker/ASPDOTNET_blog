using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DOTNET_DIARIES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        // GET: api/images
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Images API is working!");
        }
    }
}
