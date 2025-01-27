﻿using Microsoft.AspNetCore.Builder;
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
                    BusinessField = "Các sản phẩm/dịch vụ chính: Bao đay A70 dầu thực vật",
                    TaxCode = "3701716622",
                    FoundationYear = "17/09/2018",
                    HeadquartersAddress = "Khu Phố Bình Phước B, Phường Bình Chuẩn, Thành phố Thuận An, Bình Dương",
                    ManagedBy = "Chi cục Thuế khu vực Bến Cát",
                    TypeOfBusiness = "Công ty cổ phần ngoài NN",
                    PhoneNumber = "0977781184",
                    WhatsAppNumber = "0977781184",
                    ZaloNumber = "0972902066"
                };

                context.CompanyInfo.Add(company);
                await context.SaveChangesAsync();
            }
        }
    }
}
