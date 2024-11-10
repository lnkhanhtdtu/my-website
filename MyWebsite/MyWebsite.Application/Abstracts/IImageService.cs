using Microsoft.AspNetCore.Http;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Abstracts
{
    public interface IImageService
    {
        Task SaveImageProductAsync(List<IFormFile> imagesFiles, int productId, bool isUpdate, List<string>? oldImages);
        Task<Image> GetImageByIdAsync(int imageId);
        Task<IEnumerable<Image>> GetImagesByProductIdAsync(int productId);
        Task DeleteImageAsync(int imageId);
    }
}
