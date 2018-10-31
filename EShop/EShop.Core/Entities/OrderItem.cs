using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class OrderItem
  {
    public int Id { get; set; }
    public int ProductId { get; set; } // FK
    public int OrderId { get; set; } // FK
    public int Quantity { get; set; }

    // public enum Status { get;set; } TODO : Решить, как будет лучше
  }
}
