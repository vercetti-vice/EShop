using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class City : BaseEntity
  {
    public string Name { get; set; }

    public City(string name)
    {
      Name = name;
    }
  }
}
