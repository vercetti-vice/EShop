using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.Entities;
using EShop.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CityController : Controller, IRepository<City>
  {
    private IMapper _mapper;
    private ApplicationDbContext _context;

    public CityController(ApplicationDbContext context)
    {
      _context = context;
    }

    [HttpGet("getall")]
    public ActionResult GetAll()
    {
      var cities = _context.Cities;
      return Ok(cities);
    }

    [HttpGet]
    public ActionResult GetById([FromBody]int id)
    {
      throw new NotImplementedException();
    }

    [HttpPost]
    public ActionResult Create([FromBody]City item)
    {
      _context.Cities.Add(item);
      _context.SaveChanges();
      return Ok();
    }

    public ActionResult Update(City item)
    {
      throw new NotImplementedException();
    }

    public ActionResult Delete(int id)
    {
      throw new NotImplementedException();
    }
  }
}
