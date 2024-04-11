using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : _BaseController
    {
        public ProductsController(IGenericRepository<Product> genericRepository) { }


    }
}
