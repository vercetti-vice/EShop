﻿// ====================================================
 
 
// ====================================================

using System;
using System.Linq;


namespace EShop.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public string Comments { get; set; }
    }
}
