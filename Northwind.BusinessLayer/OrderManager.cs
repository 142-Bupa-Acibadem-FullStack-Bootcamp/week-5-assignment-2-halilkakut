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
using Microsoft.Extensions.DependencyInjection;


namespace Northwind.BusinessLayer
{
    public class OrderManager : BusinessLogicBase<Order,DtoOrder>,IOrderService
    {
        public readonly IOrderRepository _orderRepository;
        public OrderManager(IServiceProvider service) : base(service) 
        {
            _orderRepository = service.GetService<IOrderRepository>();
        }

        public IQueryable OrderReport(int orderId)
        {
            return _orderRepository.OrderReport(orderId);
        }
    }
}
