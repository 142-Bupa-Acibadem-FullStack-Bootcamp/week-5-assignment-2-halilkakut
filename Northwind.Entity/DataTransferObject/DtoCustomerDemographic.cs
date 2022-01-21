using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entity.DataTransferObject
{
    public class DtoCustomerDemographic :DtoBase
    {
        public string CustomerTypeId { get; set; }
        public string CustomerDesc { get; set; }
    }
}
