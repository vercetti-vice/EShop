using System;
using System.Collections.Generic;
using System.Text;
using Sieve.Attributes;

namespace EShop.Core.Entities
{
  public class Color : BaseEntity
  {
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; set; }
    public string HexCode { get; set; }

    public virtual IEnumerable<Product> Products { get; protected set; }

    public Color(string name, string hexCode)
    {
      Name = name;
      HexCode = hexCode;
    }
  }
}
