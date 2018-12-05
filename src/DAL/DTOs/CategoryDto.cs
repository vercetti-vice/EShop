using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
