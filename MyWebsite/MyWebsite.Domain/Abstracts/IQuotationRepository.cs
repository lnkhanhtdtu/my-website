using MyWebsite.Domain.Entities;
using System.Linq.Expressions;
using MyWebsite.Domain.Enums;

namespace MyWebsite.Domain.Abstracts
{
    public interface IQuotationRepository
    {
        Task<IEnumerable<Quotation>> GetAllQuotationWithProduct(Expression<Func<Quotation, bool>>? expression = null);

        Task<IEnumerable<Quotation>> GetAllQuotation(Expression<Func<Quotation, bool>> expression = null);

        Task<Quotation> GetById(int id);

        Task SoftDelete(Quotation quotation);

        Task ChangeStatus(Quotation quotation, QuotationStatus status);
    }
}
