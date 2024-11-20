using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebsite.Domain.Entities;

namespace MyWebsite.DataAccess.Configuration
{
    public static class ConfigurationDbAccess
    {
        public static void AutoMigration(this WebApplication webApplication)
        {
            using (var scope = webApplication.Services.CreateScope())
            {
                var appContext = scope.ServiceProvider.GetRequiredService<MyWebsiteContext>();

                appContext.Database.MigrateAsync().Wait();
            }
        }

        public static async Task SeedData(this WebApplication webApplication)
        {
            using var scope = webApplication.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<MyWebsiteContext>();

            // Tạo role
            var isAdminRoleExist = await roleManager.RoleExistsAsync("Admin");

            if (!isAdminRoleExist)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Tạo user
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                FirstName = "Nhựt Khánh",
                LastName = "Lê",
                Address = "Quận 12, TP. Hồ Chí Minh",
                AccessFailedCount = 0
            };

            var identityResult = await userManager.CreateAsync(adminUser, "Admin@123");
            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Khởi tạo ApplicationConfiguration nếu không tồn tại
            if (!await context.ApplicationConfigurations.AnyAsync())
            {
                var initialConfig = new ApplicationConfiguration();
                context.ApplicationConfigurations.Add(initialConfig);
                await context.SaveChangesAsync();
            }

            if (!await context.CompanyInfo.AnyAsync())
            {
                var company = new CompanyInfo()
                {
                    Name = "CÔNG TY CỔ PHẦN BAO BÌ NÔNG SẢN MIỀN NAM",
                    ShortName = "SAGRIBAGS",
                    BusinessField = "(1) Sản xuất các loại hàng dệt khác chưa được phân vào đâu\r\nChi tiết: sản xuất bao bì đay",
                    Slogan = "",
                    TaxCode = "3701716622",
                    FoundationYear = 2010, // 2010-05-11
                    HeadquartersAddress = "Lô D-1N-CN, 1Q-CN, Khu công nghiệp Mỹ Phước 3, Phường Thới Hòa, Thị xã Bến Cát, Tỉnh Bình Dương, Việt Nam",
                    PhoneNumber = "0977781184",
                    Email = "baobinongsan.miennam@gmail.com",
                    Website = "",
                    // Quản lý bởi    Chi cục Thuế khu vực Bến Cát
                    // Loại hình DN   Công ty cổ phần ngoài NN
                    // Tình trạng Đang hoạt động(đã được cấp GCN ĐKT)
                };

                context.CompanyInfo.Add(company);
                await context.SaveChangesAsync();
            }
        }
    }
}
