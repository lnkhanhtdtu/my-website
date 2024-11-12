using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize]
    public class BaseController : Controller
    { }
}
