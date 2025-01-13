using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.DTOs;
using MyWebsite.Application.DTOs.Products;
using MyWebsite.Application.DTOs.ViewModels;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

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
                var products = await _unitOfWork.ProductRepository
                    .GetAllProductWithCategory(entity => !entity.IsDeleted); // Expression<Func<Product, bool>> expression = entity => !entity.IsDeleted;

                var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

                if (productsDto == null)
                    return result;

                var totalRecords = productsDto.Count();

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
            var result = _mapper.Map<ProductViewModel>(product);
            var categoryProduct = await _unitOfWork.CategoryRepository.GetById(id);
            var imageProducts = await _unitOfWork.ImageRepository.GetImagesByProductIdAsync(id);

            if (imageProducts.Any())
            {
                result.Images = imageProducts.ToList();
            }

            if (categoryProduct != null)
            {
                result.Category = _mapper.Map<CategoryViewModel>(categoryProduct);
            }

            return result;
        }

        public async Task SaveData(ProductViewModel productViewModel, IFormFile? mainImage, List<IFormFile>? newImages, List<string>? oldImages)
        {
            var productEntity = _mapper.Map<Product>(productViewModel);

            if (mainImage is { Length: > 0 })
            {
                using (var ms = new MemoryStream())
                {
                    await mainImage.CopyToAsync(ms);
                    byte[] fileBytes = ms.ToArray();
                    productEntity.ImageData = fileBytes;
                }

                await _unitOfWork.ProductRepository.SaveData(productEntity);
            }
            else if (productEntity.Id > 0) // TODO: Cần sửa lại trường hợp update data nhưng không update hình ảnh
            {
                var existingProduct = await _unitOfWork.ProductRepository.GetById(productEntity.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = productEntity.Name;
                    existingProduct.Summary = productEntity.Summary;
                    existingProduct.Description = productEntity.Description;
                    existingProduct.CategoryId = productEntity.CategoryId;
                    existingProduct.ImageData = existingProduct.ImageData;
                    existingProduct.IsFeatured = productEntity.IsFeatured;
                    existingProduct.Price = productEntity.Price;
                    existingProduct.OldPrice = productEntity.OldPrice;

                    await _unitOfWork.ProductRepository.SaveData(existingProduct);
                }
            }
            else
            {
                throw new Exception("Lưu dữ liệu không thành công!");
            }

            await _unitOfWork.Commit(); // Lưu dữ liệu để lấy ProductId
            await _unitOfWork.ImageRepository.SaveImageProductAsync(productEntity.Id, newImages, oldImages);
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

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _unitOfWork.ProductRepository.GetAllProductWithCategory(x => !x.IsDeleted);
        }

        public async Task<IEnumerable<Product>> GetAllFeaturedProducts()
        {
            return await _unitOfWork.ProductRepository.GetAllProductWithCategory(x => !x.IsDeleted && x.IsFeatured == true);
        }

        #endregion
    }
}
