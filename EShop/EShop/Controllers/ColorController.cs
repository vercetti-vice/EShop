using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EShop.Core.DTOs;
using EShop.Core.Entities;
using EShop.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ColorController : Controller, ICrud<ColorDto>
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public ColorController(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }


    [HttpGet("getall")]
    public ActionResult GetAll()
    {
      var color = _context.Colors;
      return Ok(color);
    }

    [HttpGet("getbyid")]
    public ActionResult GetById(int id)
    {
      var color = _context.Colors.Find(id);

      if (color == null)
      {
        return NotFound();
      }

      return Ok(color);
    }

    [HttpPost("create")]
    public ActionResult Create(ColorDto item)
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

    [HttpPut("update")]
    public ActionResult Update(ColorDto item)
    {
      var isColorExist = _context.Colors.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isColorExist)
      {
        return BadRequest();
      }

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

    [HttpGet("delete")]
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
