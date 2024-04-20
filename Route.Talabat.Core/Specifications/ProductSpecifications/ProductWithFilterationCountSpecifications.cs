using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithFilterationCountSpecifications : ProductFilterationBaseSpecifications
    {
        public ProductWithFilterationCountSpecifications(ProductSpecParams productSpecParams):base(productSpecParams)
        { }

    }
}
