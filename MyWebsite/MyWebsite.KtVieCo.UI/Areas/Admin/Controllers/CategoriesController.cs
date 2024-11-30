using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CategoryId"] = await _categoryService.GetCategoriesForDropdownListAsync();
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetById(id);
            return Json(category);
        }

        [HttpPost]
        public async Task<IActionResult> GetCategoryPagination(RequestDataTable request)
        {
            var result = await _categoryService.GetCategoryPagination(request);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(CategoryViewModel category, IFormFile? postFile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Property = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                    .ToList();

                return Json(new { status = "Error", message = "Dữ liệu không hợp lệ", errors = errors });
            }

            if (category.ParentId.HasValue && category.ParentId == category.Id)
            {
                return Json(new { status = "Error", message = "Danh mục không thể là danh mục cha của chính nó" });
            }

            try
            {
                await _categoryService.SaveData(category, postFile);
                ViewData["CategoryId"] = await _categoryService.GetCategoriesForDropdownListAsync();
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
            await _categoryService.SoftDelete(id);
            ViewData["CategoryId"] = await _categoryService.GetCategoriesForDropdownListAsync();
            return Json(new { status = "Ok" });
        }
    }
}
