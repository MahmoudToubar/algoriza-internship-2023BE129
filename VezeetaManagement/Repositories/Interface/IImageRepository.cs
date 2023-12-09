using VezeetaManagement.Models.Domain;

namespace VezeetaManagement.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<TbImage> Upload(IFormFile file, TbImage image);
    }
}
