using SampleMVCSite.Contracts.Data;
using SampleMVCSite.Models;
using System;

namespace SampleMVCSite.Contracts.Repositories
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

