using MyWebsite.Domain.Entities;
using MyWebsite.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Domain.Abstracts
{
    public interface ICompanyRepository
    {
        Task<CompanyInfo> GetById(int id);

        Task SoftDelete(CompanyInfo company);

        Task SaveData(CompanyInfo company);
    }
}
