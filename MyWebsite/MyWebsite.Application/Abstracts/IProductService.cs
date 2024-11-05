using Microsoft.AspNetCore.Http;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.Products;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace MyWebsite.Application.Abstracts
{
    public interface IProductService
    {
        Task<ResponseDatatable<ProductDTO>> GetProductPagination(RequestDataTable request);
        Task<ProductViewModel> GetById(int id);
        Task SaveData(ProductViewModel category, IFormFile? postFile);
        Task SoftDelete(int id);
        // Task<IEnumerable<ProductCartDTO>> GetProductsByListId(int[] ids);
    }
}
