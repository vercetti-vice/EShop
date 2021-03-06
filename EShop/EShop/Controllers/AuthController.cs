using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using EShop.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using EShop.Services;
using EShop.Core.Dtos;
using EShop.Core.Entities;
using System.Linq;

namespace EShop.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
      _userService = userService;
      _mapper = mapper;
      _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]UserDto userDto)
    {
      var user = _userService.Authenticate(userDto.UserName, userDto.Password);

      if (user == null)
        return BadRequest(new { message = "Username or password is incorrect" });

      var userRoles = _userService.GetUserRoles(user.Id);

      var roles = _userService.GetRoles();

      
      List<Claim> claims = new List<Claim>();
      claims.Add(new Claim(ClaimTypes.Name, user.Id));
      foreach(var role in userRoles)
      {
        var rolename = roles.SingleOrDefault(x=>x.Id==role.RoleId);
        if(rolename!=null)
          claims.Add(new Claim(ClaimTypes.Role, rolename.Name));
      }

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // return basic user info (without password) and token to store client side
      return Ok(new
      {
        Id = user.Id,
        Username = user.UserName,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Token = tokenString
      });
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody]UserDto userDto)
    {
      // map dto to entity
      var user = _mapper.Map<AppUser>(userDto);

      try
      {
        // save 
        _userService.Create(user, userDto.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }

    [HttpGet]
    public IActionResult GetAll()
    {
      var x = User.IsInRole("Admin");
      var users = _userService.GetAll();
      var userDtos = _mapper.Map<IList<UserDto>>(users);
      return Ok(userDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(string id)
    {
      var user = _userService.GetById(id);
      var userDto = _mapper.Map<UserDto>(user);
      return Ok(userDto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromBody]UserDto userDto)
    {
      // map dto to entity and set id
      var user = _mapper.Map<AppUser>(userDto);
      user.Id = id;

      try
      {
        // save 
        _userService.Update(user, userDto.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        // return error message if there was an exception
        return BadRequest(new { message = ex.Message });
      }
    }
    [Authorize(Roles ="Admin")]
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      _userService.Delete(id);
      return Ok();
    }
  }
}
