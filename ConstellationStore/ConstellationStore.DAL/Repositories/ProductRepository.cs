using ConstellationStore.Contracts.Data;
using ConstellationStore.Models;
using System;

namespace ConstellationStore.Contracts.Repositories
{
    public class ProductRepository : RepositoryBase<Customer>
    {
        public ProductRepository(DataContext context)
            : base(context)
        {
            if (context == null)
                throw new ArgumentNullException();
        }
    }//end ProductRepository

}//end namespace

