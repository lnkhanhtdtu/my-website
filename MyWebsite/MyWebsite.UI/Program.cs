using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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

var app = builder.Build();

// Cấu hình auto migration của Entity Framework Core
using (var scope = app.Services.CreateScope())
{
    var appContext = scope.ServiceProvider.GetRequiredService<MyWebsiteContext>();

    appContext.Database.MigrateAsync().Wait();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

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
