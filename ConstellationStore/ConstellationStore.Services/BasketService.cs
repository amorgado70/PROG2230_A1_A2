using ConstellationStore.Contracts.Repositories;
using ConstellationStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConstellationStore.Services
{
    public class BasketService
    {
        IRepositoryBase<Basket> baskets;
        IRepositoryBase<BasketItem> basketitems;

        public const string BasketSessionName = "eShoppingBasket";

        public BasketService(IRepositoryBase<Basket> baskets, IRepositoryBase<BasketItem> basketitems)
        {
            this.baskets = baskets;
            this.basketitems = basketitems;
        }

        private Basket createNewBasket(HttpContextBase httpContext)
        {
            //create a new basket.

            //first create a new cookie.
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            //now create a new basket and set the creation date.
            Basket basket = new Basket();
            basket.OrderDate = DateTime.Now;
            basket.BasketID = Guid.NewGuid();

            //add and persist in the dabase.
            baskets.Insert(basket);
            baskets.Commit();

            //add the basket id to a cookie
            cookie.Value = basket.BasketID.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public bool AddToBasket(HttpContextBase httpContext, int productId, int quantity)
        {
            bool success = true;
            Basket basket = GetBasket(httpContext);

            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductID == productId);
            if (item == null)
            {
                item = new BasketItem()
                {
                    BasketID = basket.BasketID,
                    ProductID = productId,
                    Quantity = quantity
                };
                basket.BasketItems.Add(item);
            }
            else
            {
                item.Quantity = item.Quantity + quantity;
            }
            baskets.Commit();
            return success;
        }

        public Basket GetBasket(HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            Basket basket;
            Guid basketId;
            if (cookie != null)//checks if cookie is null
            {
                if (Guid.TryParse(cookie.Value, out basketId))
                {
                    basket = baskets.GetById(basketId);
                    if (basket == null)//basket not found in database
                        basket = createNewBasket(httpContext);
                }
                else
                {
                    basket = createNewBasket(httpContext);
                }//end inner if-else
            }//end outer if
            else
            {
                basket = createNewBasket(httpContext);
            }
            return basket;
        }

        public int QuantityInBasket(HttpContextBase httpContext)
        {
            int quantity = 0;
            Basket basket = GetBasket(httpContext);
            if (basket == null) return 0;
            quantity = basket.BasketItems.Select(c => c.Quantity).Sum();
            return quantity;
        }
        public decimal AmountInBasket(HttpContextBase httpContext)
        {
            decimal total = 0;
            Basket basket = GetBasket(httpContext);
            if (basket == null) return 0;
            var itemtotal = basket.BasketItems.Select(c => new { amount = c.Quantity * c.Product.Price });
            total = itemtotal.Select(c => c.amount).Sum();
            return total;

        }

        public BasketItem GetBasketItemById(int BasketItemID)
        {
            return basketitems.GetById(BasketItemID);
        }

        public bool RemoveFromBasket(int BasketItemID)
        {

            basketitems.Delete(basketitems.GetById(BasketItemID));
            basketitems.Commit();

            return true;
        }

    }
}
