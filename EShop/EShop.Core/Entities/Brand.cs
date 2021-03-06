using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Sieve.Attributes;

namespace EShop.Core.Entities
{
  public class Brand : BaseEntity
  {
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public virtual IEnumerable<Product> Products { get; protected set; }

    public Brand(string name, string description, string imageUrl)
    {
      Name = name;
      Description = description;
      ImageUrl = imageUrl;
    }
  }
}
