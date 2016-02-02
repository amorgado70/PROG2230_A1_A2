using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMVCSite.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        [MaxLength(255)]
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }


    }
}
