using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EShop.Core.Entities;
using EShop.Core.Entities.ViewModels;
using EShop.Helpers;
using EShop.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace EShop.Controllers
{
  [Route("api/[controller]")]
  public class AccountsController : Controller
  {
    private readonly ApplicationDbContext _appDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public AccountsController(UserManager<AppUser> userManager, IMapper mapper, ApplicationDbContext appDbContext)
    {
      _userManager = userManager;
      _mapper = mapper;
      _appDbContext = appDbContext;
    }

    // POST api/accounts
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userIdentity = _mapper.Map<AppUser>(model);

      var result = await _userManager.CreateAsync(userIdentity, model.Password);

      if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

      await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
      await _appDbContext.SaveChangesAsync();

      return new OkObjectResult("Account created");
    }
  }
}
