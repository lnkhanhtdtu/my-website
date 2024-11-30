using MyWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace MyWebsite.Domain.Abstracts
{
    public interface IBannerRepository
    {
        Task<IEnumerable<Banner>> GetAllBanner(Expression<Func<Banner, bool>> expression = null);

        Task<Banner> GetById(int id);

        Task SaveData(Banner banner);

        Task SoftDelete(Banner banner);
    }
}
