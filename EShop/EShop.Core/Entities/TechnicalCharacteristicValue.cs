using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.Entities
{
  public class TechnicalCharacteristicValue
  {
    public string TechnicalCharacteristicId { get; set; }
    public string Name { get; set; }

    public TechnicalCharacteristic TechnicalCharacteristic { get; protected set; }
  }
}
