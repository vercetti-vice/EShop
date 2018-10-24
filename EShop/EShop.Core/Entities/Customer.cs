using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class Customer
  {
    public int Id { get; set; }
    public string IdentityId { get; set; }
    public AppUser Identity { get; set; }
    public string Location { get; set; }
    public string Locale { get; set; }
    public string Gender { get; set; }
  }
}
