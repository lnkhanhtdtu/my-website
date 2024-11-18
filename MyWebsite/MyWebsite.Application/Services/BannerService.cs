using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.Banners;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Services
{
    public class BannerService : IBannerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BannerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Admin

        public async Task<ResponseDatatable<BannerDTO>> GetBannerPagination(RequestDataTable request)
        {
            var result = new ResponseDatatable<BannerDTO>();

            try
            {
                var banners = await _unitOfWork.BannerRepository.GetAllBanner(entity => !entity.IsDeleted); // Expression<Func<Banner, bool>> expression = entity => !entity.IsDeleted;

                var bannersDto = _mapper.Map<IEnumerable<BannerDTO>>(banners);

                if (bannersDto == null)
                    return result;

                var totalRecords = bannersDto.Count();

                var data = bannersDto
                    .Skip(request.SkipItems).Take(request.PageSize)
                    .OrderBy(x => x.InOrder)
                    .ToList();

                return new ResponseDatatable<BannerDTO>
                {
                    Draw = request.Draw,
                    RecordsTotal = totalRecords,
                    RecordsFiltered = totalRecords,
                    Data = data
                };
            }
            catch (Exception e)
            {
                return result;
            }
        }

        public async Task<BannerViewModel> GetById(int id)
        {
            var banner = await _unitOfWork.BannerRepository.GetById(id);
            return _mapper.Map<BannerViewModel>(banner);
        }

        public async Task SaveData(BannerViewModel banner, IFormFile? postFile)
        {
            var bannerEntity = _mapper.Map<Banner>(banner);

            if (postFile is { Length: > 0 })
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    postFile.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    bannerEntity.ImageData = fileBytes;
                }

                await _unitOfWork.BannerRepository.SaveData(bannerEntity);
            } 
            else if (bannerEntity.Id > 0) // TODO: Cần sửa lại trường hợp update data nhưng không update hình ảnh
            {
                var existingBanner = await _unitOfWork.BannerRepository.GetById(bannerEntity.Id);
                if (existingBanner != null)
                {
                    existingBanner.Title = bannerEntity.Title;
                    existingBanner.Description = bannerEntity.Description;
                    existingBanner.InOrder = existingBanner.InOrder;
                    existingBanner.IsActive = existingBanner.IsActive;

                    await _unitOfWork.BannerRepository.SaveData(existingBanner);
                }
            }

            await _unitOfWork.Commit();
        }

        public async Task SoftDelete(int id)
        {
            var banner = await _unitOfWork.BannerRepository.GetById(id);
            await _unitOfWork.BannerRepository.SoftDelete(banner);
            await _unitOfWork.Commit();
        }

        #endregion
    }
}
