using ConstellationStore.Contracts.Data;
using ConstellationStore.Models;
using System;

namespace ConstellationStore.Contracts.Repositories
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
