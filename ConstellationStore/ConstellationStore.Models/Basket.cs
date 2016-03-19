using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstellationStore.Models
{
    public class Basket
    {
        public Guid BasketID { get; set; }
        public DateTime OrderDate { get; set; }
        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
        public virtual ICollection<BasketItem> BasketItems { get; set; }
    }
}
