using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public enum CartState
  {
    Empty,
    NotEmpty,
    Paid
  }
  public class Cart : BaseEntity
  {
    public CartState State { get; set; }

  }
}
