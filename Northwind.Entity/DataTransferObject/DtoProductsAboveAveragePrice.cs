using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entity.DataTransferObject
{
    public class DtoProductsAboveAveragePrice:DtoBase
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
