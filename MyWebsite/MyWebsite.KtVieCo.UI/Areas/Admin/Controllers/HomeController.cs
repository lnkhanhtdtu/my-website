using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
