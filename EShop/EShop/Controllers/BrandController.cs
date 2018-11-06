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
  public class BrandController : Controller, ICrud<BrandDto>
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public BrandController(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet("getall")]
    public ActionResult GetAll()
    {
      var brands = _context.Brands;
      return Ok(brands);
    }

    [HttpGet("getbyid")]
    public ActionResult GetById(int id)
    {
      var brand = _context.Brands.Find(id);

      if (brand == null)
      {
        return NotFound();
      }

      return Ok(brand);
    }

    [HttpPost("create")]
    public ActionResult Create(BrandDto item)
    {
      var isBrandExist = _context.Brands.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isBrandExist)
      {
        return BadRequest();
      }

      var brand = new Brand(item.Name, item.Description, item.ImageUrl);
      _context.Brands.Add(brand);
      _context.SaveChanges();

      return Ok();
    }

    [HttpPut("update")]
    public ActionResult Update(BrandDto item)
    {
      var brand = _context.Brands.Find(item.Id);

      if (brand == null)
      {
        return NotFound();
      }

      _mapper.Map(item, brand);

      _context.Brands.Update(brand);
      _context.SaveChanges();

      return Ok();
    }

    [HttpGet("delete")]
    public ActionResult Delete(int id)
    {
      var brand = _context.Brands.Find(id);

      if (brand == null)
      {
        return NotFound();
      }

      _context.Brands.Remove(brand);
      _context.SaveChanges();

      return Ok();
    }
  }
}
