using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.UI.Controllers
{
    [Route("lien-he")]
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
