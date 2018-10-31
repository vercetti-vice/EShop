using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Product
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; } // FK
    public int BrandId { get; set; } // FK
    public int ColorId { get; set; } // FK
    public double Price { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }

    // public * Size { get; set; } TODO : Решить, какой тип использовать
    // public string Image { get; set; } TODO : Решить, какой тип использовать
  }
}
