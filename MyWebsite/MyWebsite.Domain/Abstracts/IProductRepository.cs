using MyWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace MyWebsite.Domain.Abstracts
{
    public interface IProductRepository
    {
        Task<(IEnumerable<T>, int)> GetAllProductByPagination<T>(int pageIndex, int pageSize, string keyword);
        Task<IEnumerable<Product>> GetAllProduct(Expression<Func<Product, bool>> filter = null);
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetByListId(int[] ids);
        Task SaveData(Product product);
        Task SoftDelete(Product product);
        Task<IEnumerable<Product>> GetAllProductWithCategory(Expression<Func<Product, bool>>? expression = null);
    }
}
