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
    public class OrderRepository : GenericRepository<Order>,IOrderRepository
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
        public IQueryable OrderReport(int orderId)
        {
            var result = _dbSet.Where(x=>x.OrderId==10262).SingleOrDefault();
            var t = result.OrderDetails;
            return null;
        }
    }
}
