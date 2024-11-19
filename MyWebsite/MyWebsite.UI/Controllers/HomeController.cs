using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.Abstracts;

namespace MyWebsite.UI.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IBannerService _bannerService;

        public HomeController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _bannerService.GetAll();
            return View(banners);
        }
    }
}
