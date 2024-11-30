using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

namespace MyWebsite.DataAccess.Repositories
{
    public class CompanyRepository : GenericRepository<CompanyInfo>, ICompanyRepository
    {
        public CompanyRepository(MyWebsiteContext context) : base(context)
        {
        }

        public async Task<CompanyInfo> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task<CompanyInfo> GetFirst()
        {
            return await GetSingleAsync(x => true);
        }

        public async Task SoftDelete(CompanyInfo company)
        {
            if (company == null)
            {
                throw new ArgumentException("Quotation not found.");
            }

            company.IsDeleted = true;
            await SaveData(company);
        }

        public async Task SaveData(CompanyInfo company)
        {
            if (company.Id == 0)
            {
                await CreateAsync(company);
            }
            else
            {
                company.UpdatedAt = DateTime.Now;
                Update(company);
            }
        }
    }
}