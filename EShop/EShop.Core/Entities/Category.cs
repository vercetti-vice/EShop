using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EShop.Core.Entities
{
  public class Category : BaseEntity
  {
    public string Name { get; set; }

    [ForeignKey("ParentCategory")]
    public int? ParentCategoryId { get; set; }
    public virtual Category ParentCategory { get; protected set; }
    public virtual IEnumerable<Category> ParentCategories { get; protected set; }

    public Category(int parentCategoryId, string name)
    {
      ParentCategoryId = parentCategoryId;
      Name = name;
    }
  }
}
