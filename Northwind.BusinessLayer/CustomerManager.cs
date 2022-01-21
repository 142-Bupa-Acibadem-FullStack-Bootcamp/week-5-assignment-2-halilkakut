using Northwind.BusinessLayer.Base;
using Northwind.DataAccess.Abstract;
using Northwind.Entity.DataTransferObject;
using Northwind.Entity.Models;
using Northwind.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.BusinessLayer
{
    public class CustomerManager : BusinessLogicBase<Customer,DtoCustomer>,ICustomerService
    {
        public readonly ICustomerRepository _customerRepository;

        public CustomerManager(IServiceProvider service):base(service)
        {

        }

        public IQueryable CustomerReport(int customerId)
        {
            return _customerRepository.CustomerReport(customerId);
        }
    }
}
