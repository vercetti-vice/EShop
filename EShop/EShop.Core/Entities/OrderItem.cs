using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class OrderItem : BaseEntity
  {
    public int ProductId { get; set; } // FK
    public int CartId { get; set; } // FK
    public int Quantity { get; set; }

    // public enum Status { get;set; } TODO : Решить, как будет лучше
  }
}
