using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CompanyViewModel> GetById(int id)
        {
            var company = await _unitOfWork.CompanyRepository.GetById(id);
            return _mapper.Map<CompanyViewModel>(company);
        }

        public async Task<CompanyViewModel> GetFirst()
        {
            var company = await _unitOfWork.CompanyRepository.GetFirst();
            return _mapper.Map<CompanyViewModel>(company);
        }

        public async Task SoftDelete(int id)
        {
            var company = await _unitOfWork.CompanyRepository.GetById(id);
            await _unitOfWork.CompanyRepository.SoftDelete(company);
            await _unitOfWork.Commit();
        }

        public async Task SaveData(CompanyViewModel companyViewModel, IFormFile? postFile)
        {
            var existingCompany = companyViewModel.Id != 0 ? await _unitOfWork.CompanyRepository.GetById(companyViewModel.Id) : new CompanyInfo();
            existingCompany.Name = companyViewModel.Name;
            existingCompany.ShortName = companyViewModel.ShortName;
            existingCompany.BusinessField = companyViewModel.BusinessField;
            existingCompany.TaxCode = companyViewModel.TaxCode;
            existingCompany.FoundationYear = companyViewModel.FoundationYear;
            existingCompany.HeadquartersAddress = companyViewModel.HeadquartersAddress;
            existingCompany.PhoneNumber = companyViewModel.PhoneNumber;
            existingCompany.Email = companyViewModel.Email;
            existingCompany.Website = companyViewModel.Website;
            existingCompany.ZaloOaId = companyViewModel.ZaloOaId;
            existingCompany.ZaloNumber = companyViewModel.ZaloNumber;
            existingCompany.WhatsAppNumber = companyViewModel.WhatsAppNumber;
            existingCompany.ManagedBy = companyViewModel.ManagedBy;
            existingCompany.TypeOfBusiness = companyViewModel.TypeOfBusiness;

            if (postFile is { Length: > 0 })
            {
                using var ms = new MemoryStream();
                await postFile.CopyToAsync(ms);
                byte[] fileBytes = ms.ToArray();
                existingCompany.Logo = fileBytes;
            }

            await _unitOfWork.CompanyRepository.SaveData(existingCompany);
            await _unitOfWork.Commit();
        }
    }
}
