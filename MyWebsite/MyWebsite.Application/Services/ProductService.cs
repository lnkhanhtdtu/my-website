using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs.Products;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Application.DTOs;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyWebsite.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region Admin

        public async Task<ResponseDatatable<ProductDTO>> GetProductPagination(RequestDataTable request)
        {
            try
            {
                // Cách xử lý truy vấn dữ liệu từ Stored Procedure
                //int totalRecords = 0;
                //IEnumerable<ProductDTO> products;

                //(products, totalRecords) = await _unitOfWork.ProductRepository.GetAllProductByPagination<ProductDTO>(request.SkipItems, request.PageSize, request.Search?.Value);

                //return new ResponseDatatable<ProductDTO>
                //{
                //    Draw = request.Draw,
                //    RecordsTotal = totalRecords,
                //    RecordsFiltered = totalRecords,
                //    Data = products.ToList()
                //};

                // Cách xử lý truy vấn dữ liệu từ EF Core Queryable
                var result = new ResponseDatatable<ProductDTO>();

                Expression<Func<Product, bool>> expression = entity => !entity.IsDeleted;

                var products = await _unitOfWork.ProductRepository.GetAllProductWithCategory(expression);

                var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

                if (productsDto == null)
                    return result;

                int totalRecords = productsDto.Count();

                var data = productsDto.Skip(request.SkipItems).Take(request.PageSize).ToList();

                return new ResponseDatatable<ProductDTO>
                {
                    Draw = request.Draw,
                    RecordsTotal = totalRecords,
                    RecordsFiltered = totalRecords,
                    Data = data
                };

            }
            catch (Exception e)
            {
                return new ResponseDatatable<ProductDTO>();
            }
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            return _mapper.Map<ProductViewModel>(product);
        }

        public async Task SaveData(ProductViewModel productViewModel, IFormFile? postFile)
        {
            var productEntity = _mapper.Map<Product>(productViewModel);

            if (postFile is { Length: > 0 })
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    postFile.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                    productEntity.ImageData = fileBytes;
                }

                await _unitOfWork.ProductRepository.SaveData(productEntity, postFile);
            }
            else if (productEntity.Id > 0) // TODO: Cần sửa lại trường hợp update data nhưng không update hình ảnh
            {
                var existingProduct = await _unitOfWork.ProductRepository.GetById(productEntity.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = productEntity.Name;
                    existingProduct.Description = productEntity.Description;
                    existingProduct.CategoryId = productEntity.CategoryId;
                    existingProduct.ImageData = existingProduct.ImageData;
                    // existingProduct.Images = existingProduct.Images;
                    existingProduct.IsFeatured = productEntity.IsFeatured;
                    existingProduct.Price = productEntity.Price;
                    existingProduct.OldPrice = productEntity.OldPrice;

                    await _unitOfWork.ProductRepository.SaveData(existingProduct, postFile);
                }
            }
            
            await _unitOfWork.Commit();
        }

        public async Task SoftDelete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);

            await _unitOfWork.ProductRepository.SoftDelete(product);

            await _unitOfWork.Commit();
        }

        #endregion

        #region Client

        public async Task<IEnumerable<ProductViewModel>> GetProductsForSiteAsync(string search = "", int pageNumber = 1, int pageSize = 9, int category = 0, int priceMin = -1, int priceMax = -1)
        {


            return new List<ProductViewModel>(); // Hàm này chưa hoàn thiện
        }

        //public async Task<IEnumerable<ProductCartDTO>> GetProductsByListId(int[] ids)
        //{
        //    var products = await _unitOfWork.ProductRepository.GetByListId(ids);
        //    return _mapper.Map<IEnumerable<ProductCartDTO>>(products);
        //}

        #endregion
    }
}
