using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : _BaseController
    {
        private readonly IGenericRepository<Product> _genericRepository;

        public ProductsController(IGenericRepository<Product> GenericRepository)
        {
            _genericRepository = GenericRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _genericRepository.GetAllAsync();
            return Ok(products);
        }

    }
}
