using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace EShop.Core.Entities
{
  public enum ShipmentState
  {
    InWay,
    Delivered,
    Lost
  }
  public class Shipment : BaseEntity
  {
    // public bool IsFinished { get; set; } // Подумай, нужна ли
    public string OwnerId { get; set; } // FK AppUserId
    public string CourierId { get; set; } // FK AppUserId
    public string TrackingNumber { get; set; }
    public string Details { get; set; }
    public DateTime CreatedTime { get; set; }
    public ShipmentState State { get; set; }

    public Shipment([NotNull] string ownerId, [NotNull] string courierId, string trackingNumber, string details,
      DateTime createdTime, ShipmentState state)
    {
      OwnerId = ownerId ?? throw new ArgumentNullException();
      CourierId = courierId ?? throw new ArgumentNullException();
      TrackingNumber = trackingNumber;
      Details = details;
      CreatedTime = createdTime;
      State = state;
    }
  }
}
