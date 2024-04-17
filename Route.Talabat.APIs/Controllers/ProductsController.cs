using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : _BaseController
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> GenericRepository , IMapper mapper)
        {
            _genericRepository = GenericRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            //var products = await _genericRepository.GetAllAsync();
            var specs = new ProductWithBrandAndCategorySpecifications();
            var products =await  _genericRepository.GetAllWithSpecAsync(specs);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(id);
            var product =await _genericRepository.GetWithSpecAsync(specs);
            //var product = await _genericRepository.GetAsync(id);
            if (product is null)
                return NotFound(new { Message="Not Found",StatusCode = 404 });
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }

    }
}
