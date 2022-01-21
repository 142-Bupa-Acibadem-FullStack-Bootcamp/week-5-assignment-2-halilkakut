using Microsoft.EntityFrameworkCore;
using Northwind.DataAccess.Abstract;
using Northwind.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.Entityframework.Repository
{
    public class UserRepository :GenericRepository<User>,IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }
        public User Login(User login)
        {
            var user = _dbSet.FirstOrDefault(x => x.UserCode == login.UserCode && x.Password == login.Password);
            //var user = _dbSet.Where(x => x.UserCode == login.UserCode && x.Password == login.Password).SingleOrDefault();
            return user; 
        }

        public User Register(User register)
        {
            
            _dbSet.Add(register);
            return register;
           
        }
    }
}
