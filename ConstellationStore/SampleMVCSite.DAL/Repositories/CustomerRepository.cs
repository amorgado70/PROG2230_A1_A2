using SampleMVCSite.Contracts.Data;
using SampleMVCSite.Models;
using System;

namespace SampleMVCSite.Contracts.Repositories
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
