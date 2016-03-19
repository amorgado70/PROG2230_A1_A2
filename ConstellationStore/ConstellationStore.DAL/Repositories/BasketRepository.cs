using ConstellationStore.Contracts.Data;
using ConstellationStore.Models;
using System;

namespace ConstellationStore.Contracts.Repositories
{
    public class BasketRepository : RepositoryBase<Basket>
    {
        public BasketRepository(DataContext context)
            : base(context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }
        }
    }

}
