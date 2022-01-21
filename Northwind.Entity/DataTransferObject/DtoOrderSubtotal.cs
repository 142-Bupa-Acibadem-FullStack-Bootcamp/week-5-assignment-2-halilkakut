using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entity.DataTransferObject
{
    public class DtoOrderSubtotal:DtoBase
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
