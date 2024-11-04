using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
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
        public async Task<IActionResult> SaveData(ProductViewModel product, IFormFile? postFile)
        {
            await _productService.SaveData(product, postFile);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _productService.SoftDelete(id);
            return Json(new { status = "Ok" });
        }
    }
}
