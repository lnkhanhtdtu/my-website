using Microsoft.AspNetCore.Mvc;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.Services;
using MyWebsite.Domain.Enums;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    public class QuotationsController : BaseController
    {
        private readonly IQuotationService _quotationService;

        public QuotationsController(IQuotationService quotationService)
        {
            _quotationService = quotationService;
        }

        public async Task<IActionResult> Index()
        {
            return View(new QuotationViewModel());
        }

        public async Task<IActionResult> GetById(int id)
        {
            var quotation = await _quotationService.GetById(id);
            return Json(quotation);
        }

        [HttpPost]
        public async Task<IActionResult> GetQuotationPagination(RequestDataTable request)
        {
            var result = await _quotationService.GetQuotationPagination(request);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _quotationService.SoftDelete(id);
            return Json(new { status = "Ok" });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, QuotationStatus status)
        {
            await _quotationService.ChangeStatus(id, status);
            return Json(new { status = "Ok" });
        }
    }
}
