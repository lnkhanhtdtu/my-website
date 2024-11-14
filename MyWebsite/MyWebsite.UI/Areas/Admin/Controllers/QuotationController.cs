using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    public class QuotationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
