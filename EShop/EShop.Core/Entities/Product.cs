using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Sieve.Attributes;

namespace EShop.Core.Entities
{
  public class Product : BaseEntity
  {
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; set; }
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    [ForeignKey("Brand")]
    public int BrandId { get; set; } 
    [ForeignKey("Color")]
    public int ColorId { get; set; }
    [Sieve(CanFilter = true, CanSort = true)]
    public double Price { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; } // Нужно вычислять
    public string ImageUrl { get; set; }


    public virtual Category Category { get; protected set; }
    public virtual Brand Brand { get; protected set; }
    public virtual Color Color { get; protected set; }

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
