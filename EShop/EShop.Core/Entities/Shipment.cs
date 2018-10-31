using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Shipment
  {
    public int Id { get; set; }
    public bool IsFinished { get; set; }
    public string CourierId { get; set; } // FK AppUserId
    public string TrackingNumber { get; set; }
    public string Details { get; set; }
    public DateTime CreatedTime { get; set; }

    // public enum Status {get; set;} TODO : Решить, как будет лучше
  }
}
