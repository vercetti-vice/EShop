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
  public class ProductController : Controller, ICrud<ProductDto>
  {
    private ApplicationDbContext _context;
    private IMapper _mapper;

    public ProductController(ApplicationDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    [HttpGet("getall")]
    public ActionResult GetAll()
    {
      var products = _context.Products
        .Include(x => x.Brand)
        .Include(y => y.Category)
        .Include(z => z.Color);

      return Ok(products);
    }

    [HttpGet("getbyid")]
    public ActionResult GetById(int id)
    {
      var product = _context.Products.Find(id);

      if (product == null)
      {
        return NotFound();
      }

      return Ok(product);
    }

    [HttpPost("create")]
    public ActionResult Create(ProductDto item)
    {
      var isProductExist = _context.Products.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isProductExist)
      {
        return BadRequest();
      }

      var product = new Product(item.Name, item.CategoryId, item.BrandId, item.ColorId, item.Price, item.Description, item.ImageUrl);
      _context.Products.Add(product);
      _context.SaveChanges();

      return Ok();
    }

    [HttpPut("update")]
    public ActionResult Update(ProductDto item)
    {
      var isProductExist = _context.Products.FirstOrDefault(x => x.Name == item.Name) != null;

      if (isProductExist)
      {
        return BadRequest();
      }

      var product = _context.Products.Find(item.Id);

      if (product == null)
      {
        return NotFound();
      }

      _mapper.Map(item, product);

      _context.Products.Update(product);
      _context.SaveChanges();

      return Ok();
    }

    [HttpGet("delete")]
    public ActionResult Delete(int id)
    {
      var product = _context.Products.Find(id);

      if (product == null)
      {
        return NotFound();
      }

      _context.Products.Remove(product);
      _context.SaveChanges();

      return Ok();
    }
  }
}
