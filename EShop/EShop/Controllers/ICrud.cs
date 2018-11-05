using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
  interface ICrud<T>
  {
    [HttpGet("getall")]
    ActionResult GetAll();
    [HttpGet("getbyid")]
    ActionResult GetById(int id);
    [HttpPost("create")]
    ActionResult Create(T item);
    [HttpPut("update")]
    ActionResult Update(T item);
    [HttpGet("delete")]
    ActionResult Delete(int id);
  }
}
