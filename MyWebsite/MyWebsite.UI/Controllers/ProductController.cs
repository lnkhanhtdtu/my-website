using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IProductService productService, ICategoryService categoryService, IQuotationService quotationService, IEmailService emailService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _quotationService = quotationService;
            _emailService = emailService;
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
            await _quotationService.SaveData(quotationViewModel);

            // TODO: Cần thay đổi
            var subject = $"Yêu cầu tư vấn báo giá mới từ {quotationViewModel.CustomerName}";
            var content = $@"
                <h2>Có yêu cầu tư vấn báo giá mới</h2>
                <p><strong>Tên khách hàng:</strong> {quotationViewModel.CustomerName}</p>
                <p><strong>Email:</strong> {quotationViewModel.CustomerEmail}</p>
                <p><strong>Số điện thoại:</strong> {quotationViewModel.CustomerPhone}</p>
                <p><strong>Nội dung:</strong> {quotationViewModel.Content}</p>
            ";

            var emailInfo = new EmailSetting()
            {
                Name = "Hỗ trợ",
                To = "lnkhanhtdtu@gmail.com",
                Subject = subject,
                Content = content
            };

            await _emailService.Send(emailInfo);

            return Json(new { Message = "Ok" });
        }
    }
}

