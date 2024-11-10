using Microsoft.AspNetCore.Http;
using MyWebsite.Application.Abstracts;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveImageProductAsync(List<IFormFile> imagesFiles, int productId, bool isUpdate, List<string>? oldImages)
        {
            await _unitOfWork.ImageRepository.SaveImageProductAsync(imagesFiles, productId, isUpdate, oldImages);
        }

        public async Task<Image> GetImageByIdAsync(int imageId)
        {
            return await _unitOfWork.ImageRepository.GetImageByIdAsync(imageId);
        }

        public async Task<IEnumerable<Image>> GetImagesByProductIdAsync(int productId)
        {
            return await _unitOfWork.ImageRepository.GetImagesByProductIdAsync(productId);
        }

        public async Task DeleteImageAsync(int imageId)
        {
            //await _unitOfWork.ImageRepository.DeleteImageAsync(imageId);
        }
    }
}
