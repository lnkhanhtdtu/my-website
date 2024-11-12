using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;

namespace MyWebsite.UI.Areas.Admin.Controllers
{
    public class CompanyInfoesController : BaseController
    {
        private readonly MyWebsiteContext _context;
        private readonly IMapper _mapper;

        public CompanyInfoesController(MyWebsiteContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Admin/CompanyInfoes
        public async Task<IActionResult> Index()
        {
            var company = await _context.CompanyInfo.OrderBy(x => x.Id).LastOrDefaultAsync();
            return View(_mapper.Map<CompanyViewModel>(company));
        }

        // GET: Admin/CompanyInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyInfo = await _context.CompanyInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyInfo == null)
            {
                return NotFound();
            }

            return View(companyInfo);
        }

        // GET: Admin/CompanyInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CompanyInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Logo,BusinessField,Slogan,TaxCode,FoundationYear,HeadquartersAddress,PhoneNumber,Email,Website,Id,IsDeleted,CreatedAt,UpdatedAt")] CompanyInfo companyInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyInfo);
        }

        // GET: Admin/CompanyInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyInfo = await _context.CompanyInfo.FindAsync(id);
            if (companyInfo == null)
            {
                return NotFound();
            }
            return View(companyInfo);
        }

        // POST: Admin/CompanyInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Logo,BusinessField,Slogan,TaxCode,FoundationYear,HeadquartersAddress,PhoneNumber,Email,Website,Id,IsDeleted,CreatedAt,UpdatedAt")] CompanyInfo companyInfo)
        {
            if (id != companyInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyInfoExists(companyInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(companyInfo);
        }

        // GET: Admin/CompanyInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyInfo = await _context.CompanyInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyInfo == null)
            {
                return NotFound();
            }

            return View(companyInfo);
        }

        // POST: Admin/CompanyInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyInfo = await _context.CompanyInfo.FindAsync(id);
            if (companyInfo != null)
            {
                _context.CompanyInfo.Remove(companyInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyInfoExists(int id)
        {
            return _context.CompanyInfo.Any(e => e.Id == id);
        }
    }
}
