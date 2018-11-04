using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
  interface IRepository<T>
  {
    ActionResult GetAll();
    ActionResult GetById(int id);
    ActionResult Create(T item);
    ActionResult Update(T item);
    ActionResult Delete(int id);
  }
}
