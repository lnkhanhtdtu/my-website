using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Application.Abstracts;
using MyWebsite.Application.Services;
using MyWebsite.DataAccess.Repositories;
using MyWebsite.Domain.Abstracts;
using MyWebsite.Domain.Entities;

namespace MyWebsite.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigurationIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyWebsiteContext") 
                                   ?? throw new InvalidOperationException("Connection string 'MyWebsiteContext' not found.");

            services.AddDbContext<MyWebsiteContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MyWebsiteContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyWebsite";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Admin/Authentication/Login";
                options.AccessDeniedPath = "/Admin/Authentication/AccessDenied";
            });

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            });
        }

        public static void AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<PasswordHasher<ApplicationUser>>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Thêm cấu hình Authorization cho tất cả các trang trong admin
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorizationGlobal(this IServiceCollection services)
        {
            var authorizedAdmin = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            // Cấu hình Authorization cho tất cả các trang trong admin
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedAdminPolicy", authorizedAdmin);
            });
        }
    }
}
