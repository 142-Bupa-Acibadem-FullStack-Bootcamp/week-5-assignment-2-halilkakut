using Microsoft.EntityFrameworkCore;
using Northwind.DataAccess.Abstract;
using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.Entityframework.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        #region Variables
        protected DbContext _context;
        protected DbSet<T> _dbSet;
        #endregion

        #region Constructor
        public GenericRepository(DbContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<T>();
            //this._context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;//?
        }
        #endregion

        #region Methods
        public T Add(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            return Delete(Find(id));
        }

        public bool Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }
            return _dbSet.Remove(entity) != null;
        }

        public T Find(int id)
        {
            return _dbSet.Find(id);
        }


        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        #endregion
    }
}
