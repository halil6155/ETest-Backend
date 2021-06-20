using System.IO;
using System.Threading.Tasks;
using Core.Utilities.FormFiles.Abstract;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Core.Utilities.FormFiles.SixLabors
{
    public class ImageCrud : IImageCrud
    {
        public async Task ImageUploadAsync(IFormFile formFile, string path, int width = 144, int height = 144)
        {
            using var image = await Image.LoadAsync(formFile.OpenReadStream());
            image.Mutate(x => x.Resize(width, height));
            var combinePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + path);
            await image.SaveAsync(combinePath);
        }
    }
}