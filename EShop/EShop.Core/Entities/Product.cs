using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Product : BaseEntity
  {
    public string Name { get; set; }
    public int CategoryId { get; set; } // FK
    public int BrandId { get; set; } // FK
    public int ColorId { get; set; } // FK
    public double Price { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; } // Нужно вычислять
    public string ImageUrl { get; set; }

    public Product(string name, int categoryId, int brandId, int colorId, double price, string description, string imageUrl)
    {
      Name = name;
      CategoryId = categoryId;
      BrandId = brandId;
      ColorId = colorId;

      if (price > 0)
      {
        Price = price;
      }

      Description = description;
      ImageUrl = imageUrl;
    }
  }
}
