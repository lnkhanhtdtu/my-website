using MyWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace MyWebsite.Domain.Abstracts
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategory(Expression<Func<Category, bool>> expression = null);

        Task<Category> GetById(int id);

        Task SaveData(Category category);

        Task SoftDelete(Category category);
    }
}
