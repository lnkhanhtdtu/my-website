using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyWebsite.Infrastructure.Configuration
{
    public static class ConfigurationService
    {
        public static void ConfigurationIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MyWebsiteContext") 
                                   ?? throw new InvalidOperationException("Connection string 'MyWebsiteContext' not found.");

            services.AddDbContext<MyWebsiteContext>(options => options.UseSqlServer(connectionString));

            // services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ScarAutoContext>();
            //
            // services.ConfigureApplicationCookie(options =>
            // {
            //     options.Cookie.Name = "ScarAuto";
            //     options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //     options.LoginPath = "/Admin/Authentication/Login";
            //     options.AccessDeniedPath = "/Admin/Authentication/AccessDenied";
            // });
            //
            // services.Configure<IdentityOptions>(options =>
            // {
            //     options.Lockout.AllowedForNewUsers = true;
            //     options.Lockout.MaxFailedAccessAttempts = 3;
            //     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            // });
        }

        // public static void AddDependencyInjection(this IServiceCollection services)
        // {
        //     services.AddTransient<PasswordHasher<ApplicationUser>>();
        //     services.AddTransient<ISqlQueryHandler, SQLQueryHandler>();
        //     services.AddTransient<IUnitOfWork, UnitOfWork>();
        //     services.AddTransient<IUserService, UserService>();
        //     services.AddTransient<IRoleService, RoleService>();
        //     services.AddTransient<IAuthenticationService, AuthenticationService>();
        //     services.AddTransient<IImageService, ImageService>();
        //
        //     services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        //     services.AddTransient<IPDFService, PDFService>();
        //
        //     services.AddTransient<ICategoryService, CategoryService>();
        //     services.AddTransient<IProductService, ProductService>();
        //     services.AddTransient<IOrderService, OrderService>();
        //     services.AddTransient<ICartService, CartService>();
        //     services.AddTransient<ICommonService, CommonService>();
        //
        //     services.AddTransient<IExcelhandler, Excelhandler>();
        // }

        // public static void AddAutoMapper(this IServiceCollection services)
        // {
        //     services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // }

        /// <summary>
        /// Thêm cấu hình Authorization cho tất cả các trang trong admin
        /// </summary>
        /// <param name="services"></param>
        // public static void AddAuthorizationGlobal(this IServiceCollection services)
        // {
        //     var authorizedAdmin = new AuthorizationPolicyBuilder()
        //             .RequireAuthenticatedUser()
        //             .Build();
        //
        //     // Cấu hình Authorization cho tất cả các trang trong admin
        //     services.AddAuthorization(options =>
        //     {
        //         options.AddPolicy("AuthenticatedAdminPolicy", authorizedAdmin);
        //     });
        // }
    }
}
