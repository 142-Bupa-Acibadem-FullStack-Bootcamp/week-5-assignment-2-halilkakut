using Microsoft.EntityFrameworkCore;
using Northwind.DataAccess.Abstract;
using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.Entityframework.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(DbContext context) : base(context)
        {

        }


        public IQueryable CustomerReport(int customerId)
        {
            return _dbSet.AsQueryable();
        }
    }
}
