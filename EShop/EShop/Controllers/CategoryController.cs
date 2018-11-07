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
  public class CategoryController : Controller, ICrud<CategoryDto>
  {
    private readonly ISieveProcessor _sieveProcessor;
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public CategoryController(ApplicationDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
    {
      _context = context;
      _mapper = mapper;
      _sieveProcessor = sieveProcessor;
    }


    [HttpGet]
    public ActionResult GetAll(string sorts, string filters, int page, int pageSize)
    {
      var model = new SieveModel { Sorts = sorts, Filters = filters, Page = page, PageSize = pageSize };

      var categories = _context.Categories.Include(x => x.ParentCategory).AsNoTracking();
      categories = _sieveProcessor.Apply(model, categories);
      return Ok(categories);
    }

    [HttpGet]
    public ActionResult GetById(int id)
    {
      var category = _context.Categories.Find(id);

      if (category == null)
      {
        return NotFound();
      }

      return Ok(category);
    }

    [HttpPost]
    public ActionResult Create([FromBody]CategoryDto item)
    {
      var isCategoryExist = _context.Categories.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isCategoryExist)
      {
        return BadRequest();
      }

      var category = new Category(item.Name, item.ParentCategoryId); 
      _context.Categories.Add(category);
      _context.SaveChanges();

      return Ok();
    }

    [HttpPut]
    public ActionResult Update([FromBody]CategoryDto item)
    {
      var category = _context.Categories.Find(item.Id);

      if (category == null)
      {
        return NotFound();
      }

      _mapper.Map(item, category);

      _context.Categories.Update(category);
      _context.SaveChanges();

      return Ok();
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
      var category = _context.Categories.Find(id);

      if (category == null)
      {
        return NotFound();
      }

      _context.Categories.Remove(category);
      _context.SaveChanges();

      return Ok();
    }
  }
}
