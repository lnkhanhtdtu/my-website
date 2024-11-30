using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;
using System.Linq.Expressions;

namespace MyWebsite.DataAccess.Repositories
{
    public class BannerRepository : GenericRepository<Banner>, IBannerRepository
    {
        public BannerRepository(MyWebsiteContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Banner>> GetAllBanner(Expression<Func<Banner, bool>> expression)
        {
            return await GetAllAsync(expression);
        }

        public async Task<Banner> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task SaveData(Banner banner)
        {
            if (banner.Id == 0)
            {
                await CreateAsync(banner);
            }
            else
            {
                banner.UpdatedAt = DateTime.Now;
                Update(banner);
            }
        }

        public async Task SoftDelete(Banner banner)
        {
            if (banner == null)
            {
                throw new ArgumentException($"Banner not found.");
            }

            banner.IsDeleted = true;
            await SaveData(banner);
        }
    }
}
