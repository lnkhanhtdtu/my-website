using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.Categories;

namespace MyWebsite.Application.Abstracts
{
    public interface ICategoryService
    {
        Task<ResponseDatatable<CategoryDTO>> GetCategoryPagination(RequestDataTable request);
        Task<CategoryViewModel> GetById(int id);
        Task SaveData(CategoryViewModel category, IFormFile? postFile);
        Task SoftDelete(int id);
        Task<SelectList> GetCategoriesForDropdownListAsync();
        IEnumerable<CategorySiteDTO> GetCategoriesListForSiteAsync();
    }
}
