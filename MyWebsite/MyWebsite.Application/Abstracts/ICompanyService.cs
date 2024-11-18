using Microsoft.AspNetCore.Http;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Enums;

namespace MyWebsite.Application.Abstracts
{
    public interface ICompanyService
    {
        Task<CompanyViewModel> GetById(int id);
        Task SoftDelete(int id);
        Task SaveData(CompanyViewModel companyViewModel, IFormFile? postFile);
    }
}
