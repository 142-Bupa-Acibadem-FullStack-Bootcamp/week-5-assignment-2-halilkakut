using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entity.DataTransferObject
{
    public class DtoRegion:DtoBase
    {
        public int RegionId { get; set; }
        public string RegionDescription { get; set; }
    }
}
