using ConstellationStore.Contracts.Data;
using ConstellationStore.Models;
using System;

namespace ConstellationStore.Contracts.Repositories
{
    public class Orders : RepositoryBase<Order>
    {
        public Orders(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }
}
