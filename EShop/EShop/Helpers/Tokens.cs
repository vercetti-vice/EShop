using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShop.Auth;
using EShop.Core.Entities;
using Newtonsoft.Json;

namespace EShop.Helpers
{
  public class Tokens
  {
    public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JwtIssuerOptions jwtOptions, JsonSerializerSettings serializerSettings)
    {
      var response = new
      {
        id = identity.Claims.Single(c => c.Type == "id").Value,
        auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
        expires_in = (int)jwtOptions.ValidFor.TotalSeconds
      };

      return JsonConvert.SerializeObject(response, serializerSettings);
    }
  }
}
