using ConstellationStore.Contracts.Data;

using ConstellationStore.Models;
using System;
using System.Linq;

namespace ConstellationStore.Contracts.Repositories
{
    public class CustomerRepository:RepositoryBase<Customer>
    {
        public CustomerRepository(DataContext context):base(context)
        {
            if (context==null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
