using Microsoft.AspNetCore.Http;
using VezeetaManagement.Data;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Repositories.Interface;

namespace VezeetaManagement.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly VezeetaDbContext vezeetaDbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, VezeetaDbContext vezeetaDbContext) 
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.vezeetaDbContext = vezeetaDbContext;
        }
        public async Task<TbImage> Upload(IFormFile file, TbImage image)
        {
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
            using var stream = new FileStream(localPath, FileMode.Create);
            await file.CopyToAsync(stream);

            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{image.FileName}{image.FileExtension}"; 

            image.Url = urlPath;

            await vezeetaDbContext.Image.AddAsync(image);
            await vezeetaDbContext.SaveChangesAsync();

            return image;
        }
    }
}
