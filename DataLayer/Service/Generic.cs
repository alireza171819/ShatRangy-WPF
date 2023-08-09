using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataLayer.Service
{
    public class Generic<TEntity> where TEntity : class
    {
        private ShatRangyContext _context = new ShatRangyContext();
        private DbSet<TEntity> _dbSet;
        public Generic(ShatRangyContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (where != null)
            {
                query = query.Where(where);
            }

            return query.ToList();
        }

        public virtual bool Insert(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual TEntity GetById(object id)
        {
             return _dbSet.Find(id);
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool Delete(object id)
        {
            try
            {
                var targetObject = GetById(id);
                Delete(targetObject);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public virtual bool Update(TEntity entity, int id)
        {
            try
            {
                var obj = GetById(id);
                _context.Entry(obj).CurrentValues.SetValues(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
 