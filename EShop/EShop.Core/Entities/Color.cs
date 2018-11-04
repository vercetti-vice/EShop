using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Color : BaseEntity
  {
    public string Name { get; set; }
    public string HexCode { get; set; }

    public Color(string name, string hexCode)
    {
      Name = name;
      HexCode = hexCode;
    }
  }
}
