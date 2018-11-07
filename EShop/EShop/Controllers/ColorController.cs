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
using Sieve.Models;
using Sieve.Services;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]/[action]")]
  public class ColorController : Controller, ICrud<ColorDto>
  {
    private readonly ISieveProcessor _sieveProcessor;
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public ColorController(ApplicationDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
    {
      _context = context;
      _mapper = mapper;
      _sieveProcessor = sieveProcessor;
    }


    [HttpGet]
    public ActionResult GetAll(string sorts, string filters, int page, int pageSize)
    {
      var model = new SieveModel { Sorts = sorts, Filters = filters, Page = page, PageSize = pageSize };

      var colors = _context.Colors.AsNoTracking();
      colors = _sieveProcessor.Apply(model, colors);
      return Ok(colors.ToList());
    }

    [HttpGet]
    public ActionResult GetById(int id)
    {
      var color = _context.Colors.Find(id);

      if (color == null)
      {
        return NotFound();
      }

      return Ok(color);
    }

    [HttpPost]
    public ActionResult Create([FromBody]ColorDto item)
    {
      var isColorExist = _context.Colors.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isColorExist)
      {
        return BadRequest();
      }

      var color = new Color(item.Name, item.HexCode);
      _context.Colors.Add(color);
      _context.SaveChanges();

      return Ok();
    }

    [HttpPut]
    public ActionResult Update([FromBody]ColorDto item)
    {
      var color = _context.Colors.Find(item.Id);

      if (color == null)
      {
        return NotFound();
      }

      _mapper.Map(item, color);

      _context.Colors.Update(color);
      _context.SaveChanges();

      return Ok();
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
      var color = _context.Colors.Find(id);

      if (color == null)
      {
        return NotFound();
      }

      _context.Colors.Remove(color);
      _context.SaveChanges();

      return Ok();
    }
  }
}
