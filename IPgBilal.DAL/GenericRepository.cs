using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using IPgBilal.Domain;

namespace IPgBilal.DAL
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly IpgBilalbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(IpgBilalbContext context)
        {
            _context = context;

            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string includeProperty = null)
        {
            IQueryable<T> query = _dbSet;
     
            if (filter != null && includeProperty!=null)
            {
                query = query.Where(filter).Include(includeProperty);
            }
            else if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public void Update(T entity)
        {
            _dbSet.AddOrUpdate(entity);
            _context.SaveChanges();
        }
    }
}