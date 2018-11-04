using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace EShop.Core.Entities
{
  public class Review : BaseEntity
  {
    public int ProductId { get; set; } // FK
    public string AppUserId { get; set; } // FK
    public string Description { get; set; }
    public double Rating { get; set; }
    public DateTime CreatedTime { get; set; }

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
