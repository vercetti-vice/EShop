using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.DTOs;
using EShop.Core.Entities;
using EShop.Helpers;
using EShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CityController : Controller, ICrud<CityDto>
  {
    private IMapper _mapper;
    private ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CityController(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
      _context = context;
      _mapper = mapper;
      _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("getall")]
    public ActionResult GetAll()
    {
      var cities = _context.Cities;
      return Ok(cities);
    }

    [HttpGet("getbyid")]
    public ActionResult GetById(int id)
    {
      var city = _context.Cities.Find(id);

      if (city == null)
      {
        return NotFound();
      }

      return Ok(city);
    }

    [HttpPost("create")]
    public ActionResult Create([FromBody]CityDto item)
    {
      var isCityExist = _context.Cities.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isCityExist)
      {
        return BadRequest();
      }

      var city = new City(item.Name);
      _context.Cities.Add(city);
      _context.SaveChanges();

      return Ok();
    }

    [HttpPut("update")]
    public ActionResult Update([FromBody]CityDto item)
    {
      var isCityExist = _context.Cities.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isCityExist)
      {
        return BadRequest();
      }

      var city = _context.Cities.Find(item.Id);

      if (city == null)
      {
        return NotFound();
      }

      _mapper.Map(item, city);

      _context.Cities.Update(city);
      _context.SaveChanges();

      return Ok();
    }

    [HttpGet("delete")]
    public ActionResult Delete(int id)
    {
      var city = _context.Cities.Find(id);

      if (city == null)
      {
        return NotFound();
      }

      _context.Cities.Remove(city);
      _context.SaveChanges();

      return Ok();
    }

    [HttpGet("getcity")]
    public IActionResult GetCity()
    {
      //var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
      var ip = "89.232.69.244";
      var access_key = "a4d85cc72db3faa2f8a57c56eef2b840";

      var url = string.Format("http://api.ipstack.com/{0}?access_key={1}", ip, access_key);
      string city;

      using (var client = new WebClient())
      {
        var json = client.DownloadString(url);
        dynamic tmp = JsonConvert.DeserializeObject(json);
        city = (string)tmp.city;
      }

      return Ok(city);
    }
  }
}
