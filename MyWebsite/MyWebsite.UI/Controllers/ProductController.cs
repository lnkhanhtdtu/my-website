using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Settings;
using MyWebsite.Infrastructure.Services;

namespace MyWebsite.UI.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IQuotationService _quotationService;
        private readonly IEmailService _emailService;
        
        private readonly MyWebsiteContext _context;
        
        public ProductController(IProductService productService, ICategoryService categoryService, IQuotationService quotationService, IEmailService emailService, MyWebsiteContext context)
        {
            _productService = productService;
            _categoryService = categoryService;
            _quotationService = quotationService;
            _emailService = emailService;
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = _categoryService.GetCategoriesListForSiteAsync();
            ViewBag.FeaturedProducts = await _productService.GetAllFeaturedProducts();

            var products = await _productService.GetAllProducts();

            return View(products);
        }

        [HttpGet("chi-tiet/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var attributes = await _context.ProductAttributeValues
                .Include(x => x.Product)
                .Include(x => x.ProductAttribute)
                .Where(x => !x.IsDeleted && x.ProductId == id)
                .ToListAsync();

            ViewBag.ProductAttributes = attributes;

            var product = await _productService.GetById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> SaveQuotation(QuotationViewModel quotationViewModel)
        {
            await _quotationService.SaveData(quotationViewModel);
            
            var emailInfo = Utilities.EmailHelper.EmailTemplateForQuotationNotification(quotationViewModel);

            await _emailService.Send(emailInfo);

            return Json(new { Message = "Ok" });
        }
    }
}

