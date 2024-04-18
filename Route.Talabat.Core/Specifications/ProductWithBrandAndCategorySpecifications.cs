﻿using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(string? sort):base()
        {
            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAsc":
                        OrderBy = (p => p.Price);
                        break; 
                    case "PriceDesc":
                        OrderByDesc = (p => p.Price);
                        break; 
                    default:
                        OrderBy = (p => p.Name);
                        break;

                }
            }
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
