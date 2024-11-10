using Microsoft.AspNetCore.Http;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.Products;
using MyWebsite.Application.DTOs.ViewModels;

namespace MyWebsite.Application.Abstracts
{
    public interface IProductService
    {
        Task<ResponseDatatable<ProductDTO>> GetProductPagination(RequestDataTable request);
        Task<ProductViewModel> GetById(int id);
        Task SaveData(ProductViewModel category, IFormFile? mainImage, List<IFormFile>? newImages, List<string>? oldImages);
        Task SoftDelete(int id);
        // Task<IEnumerable<ProductCartDTO>> GetProductsByListId(int[] ids);
    }
}
