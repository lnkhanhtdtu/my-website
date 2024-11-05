using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace MyWebsite.DataAccess.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        // private readonly ISqlQueryHandler _sqlQueryHandler;

        // public ProductRepository(MyWebsiteContext context, ISqlQueryHandler sqlQueryHandler) : base(context)
        // {
        //     _sqlQueryHandler = sqlQueryHandler;
        // }

        public ProductRepository(MyWebsiteContext context) : base(context)
        {  }

        public async Task<(IEnumerable<T>, int)> GetAllProductByPagination<T>(int skipItems, int pageSize, string keyword)
        {
            // DynamicParameters parameters = new DynamicParameters();
            // parameters.Add("keyword", keyword, System.Data.DbType.String, System.Data.ParameterDirection.Input);
            // parameters.Add("skipItems", skipItems, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            // parameters.Add("pageSize", pageSize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
            // parameters.Add("totalRecords", 0, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
            //
            // var result = await _sqlQueryHandler.ExecuteStoredProcedureReturnList<T>("GetAllProductByPagination", parameters);
            // var totalRecords = parameters.Get<int>("totalRecords");
            // return (result, totalRecords);

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllProduct(Expression<Func<Product, bool>> filter = null)
        {
            return await GetAllAsync(filter);
        }

        public async Task<IEnumerable<Product>> GetAllProductWithCategory(Expression<Func<Product, bool>>? expression = null)
        {
            return await GetAllWithIncludeAsync(expression, p => p.Category);
        }

        public async Task<Product> GetById(int id)
        {
            return await GetSingleAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetByListId(int[] ids)
        {
            return await GetAllAsync(x => ids.Contains(x.Id));
        }

        public async Task SaveData(Product product, IFormFile? postFile)
        {
            if (product.Id == 0)
            {
                await CreateAsync(product);
            }
            else
            {
                Update(product);
            }
        }

        public async Task SoftDelete(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException($"Product with id {product.Id} does not exist in the database. Please provide a valid product object.");
            }

            product.IsDeleted = true;
            Update(product);
        }
    }
}
