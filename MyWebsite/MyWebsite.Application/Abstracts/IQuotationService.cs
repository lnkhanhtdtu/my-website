using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Enums;

namespace MyWebsite.Application.Abstracts
{
    public interface IQuotationService
    {
        Task<ResponseDatatable<QuotationDTO>> GetQuotationPagination(RequestDataTable request);
        Task<QuotationViewModel> GetById(int id);
        Task SoftDelete(int id);
        Task ChangeStatus(int id, QuotationStatus status);
        Task SaveData(QuotationViewModel quotationViewModel);
    }
}
