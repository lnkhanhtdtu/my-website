using MyWebsite.DataAccess.Configuration;
using MyWebsite.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình Connection String cho database
builder.Services.ConfigurationIdentity(builder.Configuration);

// Đăng ký các Repositories 
builder.Services.AddDependencyInjection();

// Đăng ký AutoMapper
builder.Services.AddAutoMapper();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm cấu hình Authorization cho tất cả các trang trong admin
// builder.Services.AddAuthorizationGlobal();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

// Cấu hình auto migration của Entity Framework Core
app.AutoMigration();
app.SeedData().GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

// Cấu hình Area Routing
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
