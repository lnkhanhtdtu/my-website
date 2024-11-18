using Microsoft.AspNetCore.Mvc.Filters;
using MyWebsite.Domain.Entities;

namespace MyWebsite.UI.Utilities
{
    public class CompanyInfoFilter : IActionFilter
    {
        private readonly MyWebsiteContext _context;

        public CompanyInfoFilter(MyWebsiteContext context)
        {
            _context = context;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var companyInfo = _context.CompanyInfo.FirstOrDefault();

            context.HttpContext.Items["CompanyInfo"] = companyInfo;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
