using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;
using System.Text.RegularExpressions;

namespace MyWebsite.DataAccess.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly MyWebsiteContext _context;

        public ImageRepository(MyWebsiteContext context) : base(context)
        {
            _context = context;
        }

        public async Task SaveImageProductAsync(int productId, List<IFormFile>? newImages, List<string>? oldImages)
        {
            if (productId == 0 || (newImages == null && oldImages == null))
                return;

            var productImageClear = await _context.ProductImages.Where(x => x.ProductId == productId && !x.IsDeleted).ToListAsync();
            productImageClear.ForEach(x => x.IsDeleted = true);

            var imageIds = new List<int>();

            if (newImages != null)
            {
                foreach (var file in newImages)
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);

                    imageIds.Add(await SaveImage(file.FileName, memoryStream.ToArray()));
                }
            }

            // Xử lý các ảnh cũ dưới dạng Base64 trong oldImages
            if (oldImages != null)
            {
                foreach (var base64Image in oldImages)
                {
                    var base64Data = Regex.Replace(base64Image, "^data:image/[a-zA-Z]+;base64,", string.Empty); // Kiểm tra và tách phần tiền tố "data:image/png;base64," (nếu có)
                    var imageBytes = Convert.FromBase64String(base64Data); // Chuyển chuỗi Base64 thành byte[]

                    imageIds.Add(await SaveImage(Guid.NewGuid().ToString(), imageBytes));
                }
            }

            foreach (var imageId in imageIds)
            {
                // Tạo liên kết trong bảng ProductImage
                var productImage = new ProductImage
                {
                    ProductId = productId,
                    ImageId = imageId
                };

                _context.ProductImages.Add(productImage);
            }

            await CommitAsync();
        }

        private async Task<int> SaveImage(string name, byte[] imageBytes)
        {
            var image = new Image
            {
                Name = name,
                Data = imageBytes
            };

            await CreateAsync(image);
            await CommitAsync();

            return image.Id;
        }

        public async Task<Image> GetImageByIdAsync(int imageId)
        {
            return await GetSingleAsync(i => i.Id == imageId);
        }

        public async Task<IEnumerable<Image>> GetImagesByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId && !x.IsDeleted)
                .Select(x => x.Image)
                .ToListAsync();
        }

        public async Task DeleteImageAsync(int imageId)
        {
            var image = await GetImageByIdAsync(imageId);
            if (image != null)
            {
                // Xóa liên kết trong bảng ProductImage
                var productImages = await _context.ProductImages
                    .Where(pi => pi.ImageId == imageId)
                    .ToListAsync();
                _context.ProductImages.RemoveRange(productImages);
                Delete(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
