using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace DOTNET_DIARIES.Repositories
{

    public interface IImageRepository
    {
        /// <summary>
        /// Uploads an image file asynchronously.
        /// </summary>
        /// <param name="file">The image file to upload.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<string> UploadAsync(IFormFile file);
    }
}