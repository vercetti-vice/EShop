using System;
using System.Collections.Generic;
using System.Text;
using EShop.Core.Entities.ViewModels.Validations;
using FluentValidation.Attributes;

namespace EShop.Core.Entities.ViewModels
{
  [Validator(typeof(CredentialsViewModelValidator))]
  public class CredentialsViewModel
  {
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}
