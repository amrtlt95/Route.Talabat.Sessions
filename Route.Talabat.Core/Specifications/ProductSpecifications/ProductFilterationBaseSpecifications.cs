using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications.ProductSpecifications
{
    public abstract class ProductFilterationBaseSpecifications : BaseSpecifications<Product>
    {
        public ProductFilterationBaseSpecifications(ProductSpecParams productSpecParams) : base
            (
            p =>
                (string.IsNullOrEmpty(productSpecParams.Search)|| p.Name.ToLower().Contains(productSpecParams.Search))&&
                (!productSpecParams.BrandId.HasValue || p.BrandId == productSpecParams.BrandId) && (!productSpecParams.CategoryId.HasValue || p.CategoryId == productSpecParams.CategoryId)
            )
        { }

        public ProductFilterationBaseSpecifications(Expression<Func<Product,bool>> expression) : base(expression)
        {}
    }
}
