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
  public class BrandController : Controller, ICrud<BrandDto>
  {
    private readonly ISieveProcessor _sieveProcessor;
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public BrandController(ApplicationDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
    {
      _context = context;
      _mapper = mapper;
      _sieveProcessor = sieveProcessor;
    }

    [HttpGet]
    public ActionResult GetAll(string sorts, string filters, int page, int pageSize)
    {
      var model = new SieveModel {Sorts = sorts, Filters = filters, Page = page, PageSize = pageSize};
      var brands = _context.Brands.AsNoTracking();
      brands = _sieveProcessor.Apply(model, brands);
      return Ok(brands.ToList());
    }
    

    [HttpGet]
    public ActionResult GetById(int id)
    {
      var brand = _context.Brands.Find(id);

      if (brand == null)
      {
        return NotFound();
      }

      return Ok(brand);
    }

    [HttpPost]
    public ActionResult Create([FromBody]BrandDto item)
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

    [HttpPut]
    public ActionResult Update([FromBody]BrandDto item)
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

    [HttpGet]
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
