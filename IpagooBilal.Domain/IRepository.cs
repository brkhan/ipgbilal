using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IPgBilal.Domain
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, string includeProperty = null ); 
        void Update(T entity); 
    }
}