using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EShop.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EShop.Infrastructure.Data
{
  public class ApplicationDbContext : IdentityDbContext<AppUser, Role, string>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Color> Colors { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
    }

  }

  public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseSqlServer(connectionString);

      return new ApplicationDbContext(builder.Options);
    }
  }
}
