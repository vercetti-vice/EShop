using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using DAL.DTOs;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation;
using Sieve.Models;
using Sieve.Services;

namespace EShop.Controllers
{
    //[Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    public class ProductController : Controller
    {
        private readonly ISieveProcessor _sieveProcessor;
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public ProductController(ApplicationDbContext context, IMapper mapper, ISieveProcessor sieveProcessor)
        {
            _context = context;
            _mapper = mapper;
            _sieveProcessor = sieveProcessor;
        }

        [HttpGet]
        public ActionResult GetAll(string sorts, string filters, int page, int pageSize)
        {
            var model = new SieveModel { Sorts = sorts, Filters = filters, Page = page, PageSize = pageSize };

            var products = _context.Products.AsNoTracking(); // TODO : Подумай насчет Инклюдов
            products = _sieveProcessor.Apply(model, products);
            return Ok(products.ToList());
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult Create([FromBody]ProductDto item)
        {
            var isProductExist = _context.Products.FirstOrDefault(x => x.Name == item.Name) != null;

            if (isProductExist)
            {
                return BadRequest();
            }

            var product = new Product(item.Name, item.CategoryId, item.BrandId, item.Price, item.Description, item.ImageUrl);
            _context.Products.Add(product);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public ActionResult Update([FromBody]ProductDto item)
        {
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

        [HttpDelete]
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
