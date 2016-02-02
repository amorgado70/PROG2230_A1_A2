using SampleMVCSite.Contracts.Data;
using SampleMVCSite.Models;
using System;

namespace SampleMVCSite.Contracts.Repositories
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
