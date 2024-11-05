using MyWebsite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyWebsite.Domain.Abstracts
{
    public interface IProductRepository
    {
        Task<(IEnumerable<T>, int)> GetAllProductByPagination<T>(int pageIndex, int pageSize, string keyword);
        Task<IEnumerable<Product>> GetAllProduct(Expression<Func<Product, bool>> filter = null);
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetByListId(int[] ids);
        Task SaveData(Product product, IFormFile? postFile);
        Task SoftDelete(Product product);
        Task<IEnumerable<Product>> GetAllProductWithCategory(Expression<Func<Product, bool>>? expression = null);
    }
}
