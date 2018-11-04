using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace EShop.Core.Entities
{
  public class Category : BaseEntity
  {
    public int ParentCategoryId { get; set; } // FK
    public string Name { get; set; }

    public Category(int parentCategoryId, string name)
    {
      ParentCategoryId = parentCategoryId;
      Name = name;
    }
  }
}
