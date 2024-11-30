using MyWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyWebsite.Domain.Abstracts
{
    public interface IImageRepository
    {
        Task SaveImageProductAsync(int productId, List<IFormFile>? newImages, List<string>? oldImages);
        Task<Image> GetImageByIdAsync(int imageId);
        Task<IEnumerable<Image>> GetImagesByProductIdAsync(int productId);
        Task DeleteImageAsync(int imageId);
    }
}
