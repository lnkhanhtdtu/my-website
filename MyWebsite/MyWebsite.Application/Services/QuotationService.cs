using AutoMapper;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;
using MyWebsite.Domain.Enums;

namespace MyWebsite.Application.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public QuotationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Admin

        public async Task<ResponseDatatable<QuotationDTO>> GetQuotationPagination(RequestDataTable request)
        {
            try
            {
                // Cách xử lý truy vấn dữ liệu từ EF Core Queryable
                var result = new ResponseDatatable<QuotationDTO>();
                var quotations = await _unitOfWork.QuotationRepository
                    .GetAllQuotationWithProduct(entity => !entity.IsDeleted);

                var quotationsDto = _mapper.Map<IEnumerable<QuotationDTO>>(quotations);

                if (quotationsDto == null)
                    return result;

                var totalRecords = quotationsDto.Count();

                var data = quotationsDto.Skip(request.SkipItems).Take(request.PageSize).ToList();

                return new ResponseDatatable<QuotationDTO>
                {
                    Draw = request.Draw,
                    RecordsTotal = totalRecords,
                    RecordsFiltered = totalRecords,
                    Data = data
                };
            }
            catch (Exception e)
            {
                return new ResponseDatatable<QuotationDTO>();
            }
        }

        public async Task<QuotationViewModel> GetById(int id)
        {
            var quotation = await _unitOfWork.QuotationRepository.GetById(id);
            var result = _mapper.Map<QuotationViewModel>(quotation);
            var product = await _unitOfWork.ProductRepository.GetById(id);
            result.Product = product;

            return result;
        }

        public async Task SoftDelete(int id)
        {
            var quotation = await _unitOfWork.QuotationRepository.GetById(id);
            await _unitOfWork.QuotationRepository.SoftDelete(quotation);
            await _unitOfWork.Commit(); }

        public async Task ChangeStatus(int id, QuotationStatus status)
        {
            var quotation = await _unitOfWork.QuotationRepository.GetById(id);
            await _unitOfWork.QuotationRepository.ChangeStatus(quotation, status);
            await _unitOfWork.Commit();
        }

        public async Task SaveData(QuotationViewModel quotationViewModel)
        {
            var quotation = _mapper.Map<Quotation>(quotationViewModel);
            await _unitOfWork.QuotationRepository.SaveData(quotation);
            await _unitOfWork.Commit();
        }

        #endregion
    }
}
