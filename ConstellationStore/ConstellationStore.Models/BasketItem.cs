using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstellationStore.Models
{
    public class BasketItem
    {

        [Key]
        public int BasketItemID { get; set; }
        public Guid BasketID { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }

    }
}
