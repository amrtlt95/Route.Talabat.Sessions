using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.SqlServerDbFiles.Data
{
    public static class ApplicationContextSeed
    {
        public async static Task SeedAsync(ApplicationDbContext _dbContext)
        {
            #region BrandsSeeding
            if (!_dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeeding/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands is not null && brands.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        _dbContext.ProductBrands.Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion



            #region CategoriesSeeding
            if (!_dbContext.productCategories.Any())
            {
                var categoriesData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeeding/categories.json");
                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories is not null && categories.Count > 0)
                {
                    foreach (var category in categories)
                    {
                        _dbContext.productCategories.Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion


            #region ProductsSeeding
            if (!_dbContext.Products.Any())
            {
                var productsData = File.ReadAllText("../Route.Talabat.Infrastructure/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products is not null && products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        _dbContext.Products.Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion
        }
    }
}
