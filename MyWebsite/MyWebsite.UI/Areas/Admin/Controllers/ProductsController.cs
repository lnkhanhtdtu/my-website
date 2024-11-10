using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CategoryId"] = await _categoryService.GetCategoriesForDropdownListAsync();
            return View(new ProductViewModel());
        }

        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            return Json(product);
        }

        [HttpPost]
        public async Task<IActionResult> GetProductPagination(RequestDataTable request)
        {
            var result = await _productService.GetProductPagination(request);
            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(
            ProductViewModel product,
            IFormFile? postFile,
            List<IFormFile>? newImages,
            List<string>? oldImages)
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
                await _productService.SaveData(product, postFile, newImages, oldImages);
                return Json(new { status = "Ok", message = "Lưu dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "Error", message = "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _productService.SoftDelete(id);
            return Json(new { status = "Ok" });
        }
    }
}
