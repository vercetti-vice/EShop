using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Order
  {
    public int Id { get; set; }
    public string AppUserId { get; set; } // FK
    public int CartId { get; set; } // FK
    public int ShipmentId { get; set; } // FK
    public string Details { get; set; }
    public DateTime CreatedTime { get; set; }

    //public enum Status { get; set; } TODO : Решить, как будет лучше
  }
}
