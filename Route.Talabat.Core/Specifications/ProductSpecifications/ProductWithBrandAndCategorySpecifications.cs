using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications.ProductSpecifications
{
    public class ProductWithBrandAndCategorySpecifications : ProductFilterationBaseSpecifications
    {
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams productSpecParams) : base(productSpecParams)
        {
            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "PriceAsc":
                        OrderBy = p => p.Price;
                        break;
                    case "PriceDesc":
                        OrderByDesc = p => p.Price;
                        break;
                    default:
                        OrderBy = p => p.Name;
                        break;

                }
            }
            AddingIncludes();

            AddPagination((productSpecParams.PageIndex - 1) * productSpecParams.PageSize, productSpecParams.PageSize);
        }

        public ProductWithBrandAndCategorySpecifications(int id) : base(p => p.Id == id)
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
