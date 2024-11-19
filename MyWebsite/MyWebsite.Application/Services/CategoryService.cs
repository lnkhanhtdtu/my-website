using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.Categories;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Admin

        public async Task<ResponseDatatable<CategoryDTO>> GetCategoryPagination(RequestDataTable request)
        {
            var result = new ResponseDatatable<CategoryDTO>();

            try
            {
                var categories = await _unitOfWork.CategoryRepository.GetAllCategory(entity => !entity.IsDeleted); // Expression<Func<Category, bool>> expression = entity => !entity.IsDeleted;

                var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

                if (categoriesDto == null)
                    return result;

                var totalRecords = categoriesDto.Count();

                var data = categoriesDto
                    .Skip(request.SkipItems).Take(request.PageSize)
                    .OrderBy(x => x.ParentId).ThenBy(x => x.Name)
                    .ToList();

                return new ResponseDatatable<CategoryDTO>
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

        public async Task<CategoryViewModel> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task SaveData(CategoryViewModel category, IFormFile? postFile)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            if (postFile is { Length: > 0 })
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    postFile.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    categoryEntity.ImageData = fileBytes;
                }

                await _unitOfWork.CategoryRepository.SaveData(categoryEntity);
            } 
            else if (categoryEntity.Id > 0) // TODO: Cần sửa lại trường hợp update data nhưng không update hình ảnh
            {
                var existingCategory = await _unitOfWork.CategoryRepository.GetById(categoryEntity.Id);
                if (existingCategory != null)
                {
                    existingCategory.ParentId = categoryEntity.ParentId;
                    existingCategory.Name = categoryEntity.Name;

                    await _unitOfWork.CategoryRepository.SaveData(existingCategory);
                }
            }

            await _unitOfWork.Commit();
        }

        public async Task SoftDelete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            await _unitOfWork.CategoryRepository.SoftDelete(category);
            await _unitOfWork.Commit();
        }

        #endregion

        #region Client

        public async Task<SelectList> GetCategoriesForDropdownListAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategory(x => !x.IsDeleted && x.ParentId == null);
            return new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
        }

        public IEnumerable<CategorySiteDTO> GetCategoriesListForSiteAsync()
        {
            var result = _unitOfWork.Table<Category>().Select(x => new CategorySiteDTO
            {
                Id = x.Id,
                Name = x.Name,
                ImageData = x.ImageData,
                TotalProducts = x.Products.Count(x => !x.IsDeleted)
            });

            return result;
        }

        #endregion
    }
}
