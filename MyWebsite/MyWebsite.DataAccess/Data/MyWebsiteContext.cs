using Microsoft.EntityFrameworkCore;
using MyWebsite.Domain.Entities;

public class MyWebsiteContext : DbContext
{
    public MyWebsiteContext(DbContextOptions<MyWebsiteContext> options)
        : base(options)
    {
    }

    public DbSet<Banner> Banners { get; set; } = default!;

    public DbSet<Category> Categories { get; set; } = default!;

    public DbSet<CompanyInfo> CompanyInfo { get; set; } = default!;

    public DbSet<Image> Images { get; set; } = default!;

    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<ProductAttribute> ProductAttributes { get; set; } = default!;

    public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; } = default!;

    public DbSet<ProductImage> ProductImages { get; set; } = default!;
}