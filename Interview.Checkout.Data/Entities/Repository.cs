using Interview.Checkout.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Checkout.Data.Entities
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private FakeDbContext _dbContext { get; set; }
        public Repository(FakeDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Any(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().GetAll();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().GetAll(predicate);
        }
        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Single(predicate);
        }

       
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }
    }
}
