using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Abstracts;

namespace MyWebsite.UI.Controllers
{
    [Route("lien-he")]
    public class ContactController : Controller
    {
        private readonly ICompanyService _companyService;

        public ContactController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IActionResult> Index()
        {
            var companyInfo = await _companyService.GetFirst();
            return View(companyInfo);
        }
    }
}
