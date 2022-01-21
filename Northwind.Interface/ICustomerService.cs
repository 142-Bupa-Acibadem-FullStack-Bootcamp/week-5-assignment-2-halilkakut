using Northwind.Entity.DataTransferObject;
using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Interface
{
    public interface ICustomerService:IGenericService<Customer,DtoCustomer>
    {
        IQueryable CustomerReport(int cutomerId);
    }
}
