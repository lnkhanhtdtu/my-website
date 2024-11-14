using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.UI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
