using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    public class BannersController : BaseController
    {
        private readonly IBannerService _bannerService;

        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            var bannerViewModel = new BannerViewModel();
            return View(bannerViewModel);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var banner = await _bannerService.GetById(id);
            return Json(banner);
        }

        [HttpPost]
        public async Task<IActionResult> GetBannerPagination(RequestDataTable request)
        {
            var result = await _bannerService.GetBannerPagination(request);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(BannerViewModel banner, IFormFile? postFile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Property = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                    .ToList();

                return Json(new { status = "Error", message = "Dữ liệu không hợp lệ", errors = errors });
            }

            try
            {
                await _bannerService.SaveData(banner, postFile);
                return Json(new { status = "Ok", message = "Lưu dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { status = "Error", message = "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _bannerService.SoftDelete(id);
            return Json(new { status = "Ok" });
        }
    }
}
