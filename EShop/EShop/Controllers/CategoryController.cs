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
  public class CategoryController : Controller, ICrud<CategoryDto>
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public CategoryController(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }


    [HttpGet("getall")]
    public ActionResult GetAll()
    {
      var categories = _context.Categories.Include(x => x.ParentCategories);
      return Ok(categories);
    }

    [HttpGet("getbyid")]
    public ActionResult GetById(int id)
    {
      var category = _context.Categories.Find(id);

      if (category == null)
      {
        return NotFound();
      }

      return Ok(category);
    }

    [HttpPost("create")]
    public ActionResult Create(CategoryDto item)
    {
      var isCategoryExist = _context.Categories.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isCategoryExist)
      {
        return BadRequest();
      }

      var category = new Category(item.Name, item.ParentCategoryId); // TODO : 2 arguments
      _context.Categories.Add(category);
      _context.SaveChanges();

      return Ok();
    }

    [HttpPut("update")]
    public ActionResult Update(CategoryDto item)
    {
      var isCategoryExist = _context.Categories.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isCategoryExist)
      {
        return BadRequest();
      }

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

    [HttpGet("delete")]
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
