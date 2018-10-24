using System;
using System.Collections.Generic;
using System.Text;
using EShop.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infrastructure.Data
{
  public class ApplicationDbContext : IdentityDbContext<AppUser>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }
  }
}
