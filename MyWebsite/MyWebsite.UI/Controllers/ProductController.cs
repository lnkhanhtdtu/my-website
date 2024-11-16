using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;

namespace MyWebsite.UI.Controllers
{
    [Route("san-pham")]
    public class ProductController : Controller
    {
        private readonly MyWebsiteContext _context;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService, MyWebsiteContext context, IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _context = context;
            _mapper = mapper;
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
            var product = await _productService.GetById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> SaveQuotation(QuotationViewModel quotationViewModel)
        {
            var quotation = _mapper.Map<Quotation>(quotationViewModel);
            _context.Quotations.Add(quotation);
            await _context.SaveChangesAsync();

            return Json(new { Message = "Ok" });
        }
    }
}

