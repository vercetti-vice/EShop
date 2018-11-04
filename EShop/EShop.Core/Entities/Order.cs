using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace EShop.Core.Entities
{
  public enum OrderState
  {
    Delivered,
    NotDelivered,
    Refunded
  }
  public class Order : BaseEntity
  {
    public string AppUserId { get; set; } // FK
    public int CartId { get; set; } // FK
    public int ShipmentId { get; set; } // FK
    public string Details { get; set; }
    public DateTime CreatedTime { get; set; }
    public OrderState State { get; set; }

    public Order([NotNull]string appUserId, int cartId, int shipmentId, string details, DateTime createdTime, OrderState state)
    {
      AppUserId = appUserId ?? throw new ArgumentNullException();
      CartId = cartId;
      ShipmentId = shipmentId;
      Details = details;
      CreatedTime = createdTime;
      State = state;
    }
  }
}
