using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Domain.Entities;

public class MyWebsiteContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public MyWebsiteContext(DbContextOptions<MyWebsiteContext> options) : base(options)
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

    public DbSet<Quotation> Quotations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // AspNetUser
        // builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
    }
}