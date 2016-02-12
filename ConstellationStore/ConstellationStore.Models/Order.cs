using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstellationStore.Models
{
    public class Order
    {
        public Order()
        {
            OrderDate = DateTime.Now;
        }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
