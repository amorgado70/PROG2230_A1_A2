using System;
using System.Collections.Generic;

namespace SampleMVCSite.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
