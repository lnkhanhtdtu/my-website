﻿using AutoMapper;
using MyWebsite.Application.Abstracts;
using MyWebsite.Domain.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Entities;
using Microsoft.AspNetCore.Http;
using static System.Runtime.CompilerServices.RuntimeHelpers;

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
            existingCompany.BusinessField = companyViewModel.BusinessField;
            existingCompany.Slogan = companyViewModel.Slogan;
            existingCompany.TaxCode = companyViewModel.TaxCode;
            existingCompany.FoundationYear = companyViewModel.FoundationYear;
            existingCompany.HeadquartersAddress = companyViewModel.HeadquartersAddress;
            existingCompany.PhoneNumber = companyViewModel.PhoneNumber;
            existingCompany.Email = companyViewModel.Email;
            existingCompany.Website = companyViewModel.Website;

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