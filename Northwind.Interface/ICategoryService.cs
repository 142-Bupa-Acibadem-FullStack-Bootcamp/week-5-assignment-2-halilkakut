using Northwind.Entity.DataTransferObject;
using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Interface
{
    public interface ICategoryService : IGenericService<Category, DtoCategory>
    {
    }
}
