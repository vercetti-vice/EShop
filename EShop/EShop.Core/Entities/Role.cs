using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EShop.Core.Entities
{
  public class Role : IdentityRole<string>
  {
    public Role()
    {
    }

    public Role(string roleName) : base(roleName)
    {
    }
  }
}
