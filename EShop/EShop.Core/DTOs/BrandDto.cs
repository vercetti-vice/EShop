using System;
using System.Collections.Generic;
using System.Text;
using EShop.Core.Entities;

namespace EShop.Core.DTOs
{
  public class BrandDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public IEnumerable<Product> Products { get; set; }
  }
}
