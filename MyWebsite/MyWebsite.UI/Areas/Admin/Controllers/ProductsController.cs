using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;
using Newtonsoft.Json;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CategoryId"] = await _categoryService.GetCategoriesForDropdownListAsync();
            var productViewModel = new ProductViewModel();
            return View(productViewModel);
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
        public async Task<IActionResult> SaveData(ProductViewModel product, IFormFile? postFile, List<IFormFile> productImages)
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
                // https://christianbayer.github.io/image-uploader/#example-1
                // https://www.jqueryscript.net/demo/ajax-file-uploader/
                // https://www.jqueryscript.net/demo/drag-drop-image-uploader/
                    if (ProductImages != null && ProductImages.Count > 0)
                        {
                            foreach (var file in ProductImages)
                            {
                                // Xử lý file ở đây
                                // Ví dụ: lưu file, thêm vào database, etc.
                            }
                        }

                await _productService.SaveData(product, postFile);


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
            await _productService.SoftDelete(id);
            return Json(new { status = "Ok" });
        }
    }
}
