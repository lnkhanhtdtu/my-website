using Microsoft.AspNetCore.Mvc;
using MyWebsite.UI.Models;
using System.Diagnostics;
using MyWebsite.Application.Abstracts;

namespace MyWebsite.UI.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBannerService _bannerService;

        public HomeController(ILogger<HomeController> logger, IBannerService bannerService)
        {
            _logger = logger;
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            var banners = await _bannerService.GetAll();
            return View(banners);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
