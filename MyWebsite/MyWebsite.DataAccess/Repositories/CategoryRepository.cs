using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace MyWebsite.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MyWebsiteContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategory(Expression<Func<Category, bool>> expression)
        {
            return await GetAllAsync(expression);
        }

        public async Task<Category> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task SaveData(Category category)
        {
            if (category.Id == 0)
            {
                await CreateAsync(category);
            }
            else
            {
                category.UpdatedAt = DateTime.Now;
                Update(category);
            }
        }

        public async Task SoftDelete(Category category)
        {
            if (category == null)
            {
                throw new ArgumentException($"Category not found.");
            }

            category.IsDeleted = true;
            await SaveData(category);
        }
    }
}
