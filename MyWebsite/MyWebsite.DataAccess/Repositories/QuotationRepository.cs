using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;
using System.Linq.Expressions;
using MyWebsite.Domain.Enums;

namespace MyWebsite.DataAccess.Repositories
{
    public class QuotationRepository : GenericRepository<Quotation>, IQuotationRepository
    {
        public QuotationRepository(MyWebsiteContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Quotation>> GetAllQuotationWithProduct(Expression<Func<Quotation, bool>>? expression = null)
        {
            return await GetAllWithIncludeAsync(expression, p => p.Product);
        }

        public async Task<IEnumerable<Quotation>> GetAllQuotation(Expression<Func<Quotation, bool>> expression)
        {
            return await GetAllAsync(expression);
        }

        public async Task<Quotation> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task SaveData(Quotation category)
        {
            if (category.Id == 0)
            {
                await CreateAsync(category);
            }
            else
            {
                category.UpdatedAt = DateTime.Now;
                Update(category);
            }
        }

        public async Task SoftDelete(Quotation quotation)
        {
            if (quotation == null)
            {
                throw new ArgumentException("Quotation not found.");
            }

            quotation.IsDeleted = true;
            await SaveData(quotation);
        }

        public async Task ChangeStatus(Quotation quotation, QuotationStatus status)
        {
            if (quotation == null)
            {
                throw new ArgumentException("Quotation not found.");
            }

            quotation.Status = status;
            quotation.ResponseDate = DateTime.Now;

            await SaveData(quotation);
        }
    }
}
