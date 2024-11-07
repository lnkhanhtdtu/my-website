using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

namespace MyWebsite.DataAccess.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private readonly MyWebsiteContext _context;

        public ImageRepository(MyWebsiteContext context) : base(context)
        {
            _context = context;
        }

        public async Task SaveImageProductAsync(List<IFormFile>? imagesFiles, int productId, bool isUpdate)
        {
            if (imagesFiles is { Count: > 0 } && productId != 0)
            {
                if (isUpdate)
                {
                    var productImageClear = _context.ProductImages.Where(x => x.ProductId == productId && !x.IsDeleted);
                    await productImageClear.ForEachAsync(x => x.IsDeleted = true);
                    // _context.ProductImages.RemoveRange(productImageClear);
                    await _context.SaveChangesAsync();
                }

                foreach (var file in imagesFiles)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var image = new Image
                        {
                            Name = file.FileName,
                            Data = memoryStream.ToArray()
                        };

                        await CreateAsync(image);
                        await _context.SaveChangesAsync();

                        // Tạo liên kết trong bảng ProductImage
                        var productImage = new ProductImage
                        {
                            ProductId = productId,
                            ImageId = image.Id
                        };
                        _context.ProductImages.Add(productImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task<Image> GetImageByIdAsync(int imageId)
        {
            return await GetSingleAsync(i => i.Id == imageId);
        }

        public async Task<IEnumerable<Image>> GetImagesByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .Select(pi => pi.Image)
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
