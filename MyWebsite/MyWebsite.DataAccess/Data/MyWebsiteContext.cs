using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Domain.Entities;

public class MyWebsiteContext : DbContext
{
    public MyWebsiteContext(DbContextOptions<MyWebsiteContext> options)
        : base(options)
    {
    }

    public DbSet<MyWebsite.Domain.Entities.Category> Category { get; set; } = default!;

    public DbSet<MyWebsite.Domain.Entities.CompanyInfo> CompanyInfo { get; set; } = default!;

    public DbSet<MyWebsite.Domain.Entities.Product> Product { get; set; } = default!;
}