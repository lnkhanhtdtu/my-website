using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MyWebsite.Domain.Abstracts;

namespace MyWebsite.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        MyWebsiteContext _context;

        ICategoryRepository _categoryRepository;
        IProductRepository _productRepository;
        IImageRepository _imageRepository;

        IDbContextTransaction _dbContextTransaction;
        private bool _disposedValue;

        public UnitOfWork(MyWebsiteContext context)
        {
            _context = context;
        }

        public DbSet<T> Table<T>() where T : class => _context.Set<T>();

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);
        public IImageRepository ImageRepository => _imageRepository ??= new ImageRepository(_context);

        public async Task BeginTransaction()
        {
            _dbContextTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _dbContextTransaction?.CommitAsync();
        }

        public async Task RollbackTransaction()
        {
            await _dbContextTransaction?.RollbackAsync();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
