using System;
using System.Collections.Generic;
using System.Text;
using Sieve.Attributes;

namespace DAL.Models
{
    public class Brand : BaseEntity
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        

        public Brand(string name, string description, string imageUrl)
        {
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }
}
