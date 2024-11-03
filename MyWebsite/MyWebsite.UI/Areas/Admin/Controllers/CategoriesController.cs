using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
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
            await _categoryService.SaveData(category, postFile);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _categoryService.SoftDelete(id);
            return Json(new { status = "Ok" });
        }
    }
}
