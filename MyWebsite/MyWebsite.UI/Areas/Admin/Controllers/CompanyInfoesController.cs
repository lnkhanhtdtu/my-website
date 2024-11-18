using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Application.Services;
using MyWebsite.Domain.Entities;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    public class CompanyInfoesController : BaseController
    {
        private readonly MyWebsiteContext _context;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;

        public CompanyInfoesController(MyWebsiteContext context, IMapper mapper, ICompanyService companyService)
        {
            _context = context;
            _mapper = mapper;
            _companyService = companyService;
        }

        // GET: Admin/CompanyInfoes
        public async Task<IActionResult> Index()
        {
            var company = await _context.CompanyInfo.OrderBy(x => x.Id).LastOrDefaultAsync();
            var companyViewModel = company != null
                ? _mapper.Map<CompanyViewModel>(company)
                : _mapper.Map<CompanyViewModel>(new CompanyViewModel());

            return View(companyViewModel);
        }

        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetById(id);
            return Json(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveData(CompanyViewModel companyViewModel, IFormFile? postFile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Property = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                    .ToList();

                return Json(new { status = "Error", message = "Dữ liệu không hợp lệ", errors = errors });
            }

            try
            {
                await _companyService.SaveData(companyViewModel, postFile);
                return Json(new { status = "Ok", message = "Lưu dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { status = "Error", message = "Có lỗi xảy ra khi lưu dữ liệu: " + ex.Message });
            }
        }

    }
}
