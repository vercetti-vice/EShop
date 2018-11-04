using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
    [ForeignKey("AppUser")]
    public string AppUserId { get; set; }
    [ForeignKey("Cart")]
    public int CartId { get; set; }
    [ForeignKey("Shipment")]
    public int ShipmentId { get; set; }
    public string Details { get; set; }
    public DateTime CreatedTime { get; set; }
    public OrderState State { get; set; }

    public virtual AppUser AppUser { get; protected set; }
    public virtual Cart Cart { get; protected set; }
    public virtual Shipment Shipment { get; protected set; }

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
