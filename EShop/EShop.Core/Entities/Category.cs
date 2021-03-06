using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sieve.Attributes;

namespace EShop.Core.Entities
{
  public class Category : BaseEntity
  {
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; set; }

    [ForeignKey("ParentCategories")]
    public int? ParentCategoryId { get; set; }
    public virtual Category ParentCategory { get; protected set; }
    public virtual IEnumerable<Product> Products { get; protected set; }

    public Category(string name, int? parentCategoryId)
    {
      Name = name;
      ParentCategoryId = parentCategoryId;
    }
  }
}
