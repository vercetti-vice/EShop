using System;
using System.Collections.Generic;
using System.Text;
using EShop.Core.Entities;

namespace EShop.Core.DTOs
{
  public class ProductDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; } 
    public string ImageUrl { get; set; }

    public Brand Brand { get; set; }
    public Category Category { get; set; }
  }
}
