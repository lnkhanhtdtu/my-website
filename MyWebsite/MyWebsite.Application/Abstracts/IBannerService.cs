using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.Banners;
using MyWebsite.Application.DTOs.ViewModels;

namespace MyWebsite.Application.Abstracts
{
    public interface IBannerService
    {
        Task<ResponseDatatable<BannerDTO>> GetBannerPagination(RequestDataTable request);
        Task<BannerViewModel> GetById(int id);
        Task SaveData(BannerViewModel banner, IFormFile? postFile);
        Task SoftDelete(int id);
        // Task<SelectList> GetCategoriesForDropdownListAsync();
    }
}
