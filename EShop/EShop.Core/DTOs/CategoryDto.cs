using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.DTOs
{
  public class CategoryDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int ParentCategoryId { get; set; }
  }
}
