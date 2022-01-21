using AutoMapper;
using Northwind.Entity.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.BusinessLayer
{
    internal class ObjectMapper //Bulunduğu Projede çalışacak o yüzden internal
    {
        static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg=> {
                cfg.AddProfile<MappingProfile>();  // Profile çeşitliliği arttıkça hepsini burda yükleyebiliriz
            });

            return config.CreateMapper();  //.NET5.0 ile geldi
        });

        public static IMapper Mapper => lazy.Value;
    }
}
