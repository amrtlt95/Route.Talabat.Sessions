using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helpers;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.Repositories.Contract;
using Route.Talabat.Core.Specifications.ProductSpecifications;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : _BaseController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product> ProductRepo , IMapper mapper , IGenericRepository<ProductCategory> categoryRepo , IGenericRepository<ProductBrand> brandRepo)
        {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _categoryRepo = categoryRepo;
            _brandRepo = brandRepo;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            //var products = await _genericRepository.GetAllAsync();
            var specs = new ProductWithBrandAndCategorySpecifications(productSpecParams);
            var countSpecs = new ProductWithFilterationCountSpecifications(productSpecParams);
            var products =await  _productRepo.GetAllWithSpecAsync(specs);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
            var count = await _productRepo.GetCountWithSpecAsync(countSpecs);
            return Ok(
                new Pagination<ProductToReturnDto>(productSpecParams.PageSize, productSpecParams.PageIndex,count,data)
                );
        }


        [HttpGet("{id}")]
        [ProducesResponseType<ProductToReturnDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ApiResponse>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specs = new ProductWithBrandAndCategorySpecifications(id);
            var product =await _productRepo.GetWithSpecAsync(specs);
            //var product = await _genericRepository.GetAsync(id);
            if (product is null)
                return NotFound(new ApiResponse(404));
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }

        //categories
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }


        //brand
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

    }
}
