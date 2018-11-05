using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.DTOs;
using EShop.Core.Entities;
using EShop.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CityController : Controller, ICrud<CityDto>
  {
    private IMapper _mapper;
    private ApplicationDbContext _context;

    public CityController(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
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
  }
}
