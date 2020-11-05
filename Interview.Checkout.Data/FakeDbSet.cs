using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Checkout.Data
{
    public class FakeDbSet<T> : DbSet<T>, IDbSet<T> where T : class
    {
        List<T> _data;

        public FakeDbSet()
        {
            _data = new List<T>();
        }

        public IQueryable<T> GetAll() => _data.AsQueryable();

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _data.AsQueryable().Where(predicate);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _data.AsQueryable().Any<T>(predicate);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _data.AsQueryable().Single<T>(predicate);
        }
        public override T Add(T item)
        {
            _data.Add(item);
            return item;
        }
        public override IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _data.AddRange(entities);
            return _data;
        }

        public override IEnumerable<T> RemoveRange(IEnumerable<T> entities)
        {
            for (int i = entities.Count() - 1; i >= 0; i--)
            {
                T entity = entities.ElementAt(i);
                if (_data.Contains(entity))
                {
                    Remove(entity);
                }
            }

            return this;
        }
        
    }
}
