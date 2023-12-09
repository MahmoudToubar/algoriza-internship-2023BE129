using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VezeetaManagement.Models.Domain;
using VezeetaManagement.Models.DTO;
using VezeetaManagement.Repositories.Interface;

namespace VezeetaManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository) 
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if(ModelState.IsValid) 
            {
                var image = new TbImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreate = DateTime.Now,
                };

                image = await imageRepository.Upload(file, image);

                var response = new TbImageDto
                {
                    Id = image.Id,
                    Title = image.Title,
                    DateCreate = image.DateCreate,
                    FileExtension = image.FileExtension,
                    FileName = image.FileName,
                    Url = image.Url
                };

                return Ok(image);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(IFormFile file) 
        {
            var allowedExtention = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtention.Contains(Path.GetExtension(file.FileName).ToLower())) 
            {
                ModelState.AddModelError("file", "Unsupported file format");
            }

            if (file.Length > 10485760) 
            {
                ModelState.AddModelError("file", "File size cannot be more than 10MB");
            }
        }
    }
}
