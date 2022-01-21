using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete.Entityframework.Repository;
using Northwind.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.Entityframework.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Variables
        DbContext _context;
        IDbContextTransaction  _dbTransaction;
        bool _dispose;
        #endregion
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }
        public bool BeginTransaction()
        {
            try
            {
                _dbTransaction = _context.Database.BeginTransaction();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._dispose)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._dispose = true;
        }

        //Belleği  Temizleme
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> GetRepository<T>() where T : EntityBase
        {
            return new GenericRepository<T>(_context);
        }

        public bool RollBackTransaction()
        {
            try
            {
                _dbTransaction.Rollback();
                _dbTransaction = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int SaveChanges()
        {
            var _transaction = _dbTransaction != null ? _dbTransaction : _context.Database.BeginTransaction();
            using (_transaction)
            {
                try
                {
                    if(_context == null)
                    {
                        throw new ArgumentException("Context is null");
                    }
                    
                    int result = _context.SaveChanges();

                    _transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    //Hata olduğunda geri alır
                    _dbTransaction.Rollback();
                    throw new Exception("Error on save changes", ex);
                }
            }
        }
    }
}
