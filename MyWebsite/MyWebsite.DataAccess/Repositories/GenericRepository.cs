using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MyWebsite.DataAccess.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly MyWebsiteContext _context;

        public GenericRepository(MyWebsiteContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null)
        {
            if (expression == null)
            {
                return await _context.Set<T>().ToListAsync();
            }

            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludeAsync(
            Expression<Func<T, bool>>? expression = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
