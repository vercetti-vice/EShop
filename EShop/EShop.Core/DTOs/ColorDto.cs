using System;
using System.Collections.Generic;
using System.Text;
using EShop.Core.Entities;

namespace EShop.Core.DTOs
{
  public class ColorDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string HexCode { get; set; }

    public IEnumerable<Product> Products { get; set; }
  }
}
