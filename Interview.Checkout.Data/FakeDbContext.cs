using Interview.Checkout.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Checkout.Data
{
    public class FakeDbContext
    {
        private IDictionary<Type, object> _sets;
        public virtual FakeDbSet<T> Set<T>() where T : class
        {
            Type type = typeof(FakeDbSet<T>);
           
            if (!_sets.TryGetValue(type, out var set))
            {
                throw new TypeUnloadedException();
            }
            return (FakeDbSet<T>)set;
        }
                      
        public FakeDbContext()
        {
            Products = new FakeDbSet<Product>();
            Discounts = new FakeDbSet<Discount>();
            SetTheSets();
        }
        public virtual FakeDbSet<Product> Products { get; set; }
        public virtual FakeDbSet<Discount> Discounts { get; set; }

        private void SetTheSets()
        {
            _sets = new Dictionary<Type, object>();
            _sets.Add(typeof(FakeDbSet<Product>), Products);
            _sets.Add(typeof(FakeDbSet<Discount>), Discounts);
        }
    }
}
