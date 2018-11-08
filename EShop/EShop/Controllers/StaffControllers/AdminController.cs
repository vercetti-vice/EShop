using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EShop.Core.Entities;
using EShop.Infrastructure.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace EShop.Controllers.StaffControllers
{
  //[Authorize(Roles ="Admin")]
  [Route("api/[controller]")]
  [ApiController]
  public class AdminController : ControllerBase
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public AdminController(ApplicationDbContext context,IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetRoles()
    {
      return new JsonResult(_context.Roles.ToList());
    }


    [HttpGet]
    public IActionResult GetUser(string login)
    {
      var role = _context.UserRoles.ToList();

      var user = _context.Users.FirstOrDefault(x => x.UserName == login);
      if (user == null)
        return NotFound();
      return new JsonResult(user);
    }

    [HttpGet]
    public IActionResult RemoveRole(string UserId, string roleId)
    {
      var user = _context.Users.FirstOrDefault(x => x.Id == UserId);
      if (user == null)
        return NotFound();

      var userRole = _context.UserRoles.FirstOrDefault(x => x.UserId == UserId && x.RoleId == roleId);
      if (userRole != null)
      {
        _context.Remove(userRole);
        _context.SaveChanges();
        return Ok();
      }
      return Ok();
    }

    [HttpGet]
    public IActionResult SetRole(string Id, string roleId)
    {
      var user = _context.Users.FirstOrDefault(x => x.Id == Id);
      if (user == null)
        return NotFound();
      if (_context.UserRoles.Where(x => x.UserId == Id && x.RoleId == roleId).Count()<1)
      {
        _context.UserRoles.Add(new IdentityUserRole<string>() { RoleId = roleId, UserId = Id });
        _context.SaveChanges();
        return Ok();
      }
      return Ok();
      
    }
  }
}
