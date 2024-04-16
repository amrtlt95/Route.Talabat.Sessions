using Route.Talabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        //public Expression<Predicate<T>>? Criteria { get ; set; } = null;
        public Expression<Func<T,bool>>? Criteria { get ; set; } = null;
        public List<Expression<Func<T, object>>> Includes { get; set; }

        public BaseSpecifications()
        {
            
            Includes = [];
        }
        public BaseSpecifications(Expression<Func<T,bool>> criteria):this()
        {
            Criteria = criteria;
            
        }

    }
}
