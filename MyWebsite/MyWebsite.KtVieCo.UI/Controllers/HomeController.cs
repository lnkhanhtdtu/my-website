using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Abstracts;

namespace MyWebsite.UI.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IBannerService bannerService, IProductService productService, ICategoryService categoryService)
        {
            _bannerService = bannerService;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _bannerService.GetAll();
            ViewBag.Categories = _categoryService.GetCategoriesListForSiteAsync();
            ViewBag.FeaturedProducts = await _productService.GetAllFeaturedProducts();
            return View(banners);
        }
    }
}
