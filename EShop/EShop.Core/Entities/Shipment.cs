using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    [ForeignKey("Owner")]
    public string OwnerId { get; set; } 
    [ForeignKey("Courier")]
    public string CourierId { get; set; }
    public string TrackingNumber { get; set; }
    public string Details { get; set; }
    public DateTime CreatedTime { get; set; }
    public ShipmentState State { get; set; }

    // TODO : Нужно очень хорошо подумать
    public virtual AppUser Owner { get; protected set; }
    public virtual AppUser Courier { get; protected set; }


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
