using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications():base()
        {
            AddingIncludes();
        }

        public ProductWithBrandAndCategorySpecifications(int id):base(p=>p.Id == id)
        {
            AddingIncludes();
        }

        #region Helper methods
        private void AddingIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
        #endregion

    }
}
