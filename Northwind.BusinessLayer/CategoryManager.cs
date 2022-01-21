using Northwind.BusinessLayer.Base;
using Northwind.Entity.DataTransferObject;
using Northwind.Entity.Models;
using Northwind.Interface;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.BusinessLayer
{
    public class CategoryManager : BusinessLogicBase<Category, DtoCategory>,ICategoryService
    {
        public readonly ICategoryRepository _categoryRepository;

        public CategoryManager(IServiceProvider service) : base(service)
        {
            _categoryRepository = service.GetService<ICategoryRepository>();
        }

    }
}
