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
    public class Student
    {
        public int StudentId { get; set; }
        [Display(Name = "Name")]
        public string StudentName { get; set; }
        public Gender StudentGender { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}
