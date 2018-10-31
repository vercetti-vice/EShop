using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Category
  {
    public int Id { get; set; }
    public int ParentCategory { get; set; } // FK
    public string Name { get; set; }
  }
}
