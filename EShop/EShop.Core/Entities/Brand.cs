using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Brand : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public Brand(string name, string description, string imageUrl)
    {
      Name = name;
      Description = description;
      ImageUrl = imageUrl;
    }
  }
}
