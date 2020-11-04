using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Checkout.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        bool Any(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate);
    }
}
