using RTDS.Domain.Abstract;
using RTDS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTDS.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RTDS.Domain.Entities.RTDSModel _context = new RTDSModel();
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private bool _disposed = false;

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return _repositories[typeof(TEntity)] as IRepository<TEntity>;
           
            RTDSRepository<TEntity> repository = new RTDSRepository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);

            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
                return;
            if (disposing)
            {
                _context.Dispose();
            }
            this._disposed = true;
        }
    }
}
