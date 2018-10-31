using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Review
  {
    public int Id { get; set; }
    public int ProductId { get; set; } // FK
    public string AppUserId { get; set; } // FK
    public string Description { get; set; }
    public double Rating { get; set; }
    public DateTime CreatedTime { get; set; }
  }
}
