using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.UI.Controllers
{
    [Route("gioi-thieu")]
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
