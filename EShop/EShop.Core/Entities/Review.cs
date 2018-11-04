using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EShop.Core.Entities
{
  public class Review : BaseEntity
  {
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    [ForeignKey("AppUser")]
    public string AppUserId { get; set; }
    public string Description { get; set; }
    public double Rating { get; set; }
    public DateTime CreatedTime { get; set; }

    public virtual Product Product { get; protected set; }
    public virtual AppUser AppUser { get; protected set; }

    public Review(int productid,[NotNull] string appUserId, string description, double rating, DateTime createdTime)
    {
      ProductId = productid;
      AppUserId = appUserId ?? throw new ArgumentNullException();
      Description = description;
      Rating = rating;
      CreatedTime = createdTime;
    }
  }
}
