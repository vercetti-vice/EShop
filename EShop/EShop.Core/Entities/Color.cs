using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  // TODO : Подумай, нужна ли эта таблица
  public class Color
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string HexCode { get; set; }
  }
}
