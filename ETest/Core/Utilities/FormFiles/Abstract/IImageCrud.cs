using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Utilities.FormFiles.Abstract
{
    public interface IImageCrud
    {
        Task ImageUploadAsync(IFormFile formFile, string path, int width = 144, int height = 144);
    }
}