using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyWebsite.Domain.Abstracts
{
    public interface IUnitOfWork
    {
        DbSet<T> Table<T>() where T : class;

        ICategoryRepository CategoryRepository { get; }

        IProductRepository ProductRepository { get; }

        IImageRepository ImageRepository { get; }

        IQuotationRepository QuotationRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IBannerRepository BannerRepository { get; }

        // IOrderRepository OrderRepository { get; }
        // ICartRepository CartRepository { get; }
        // IUserAddressRepository UserAddressRepository { get; }

        Task BeginTransaction();
        Task CommitTransactionAsync();
        Task RollbackTransaction();
        Task Commit();
    }
}
