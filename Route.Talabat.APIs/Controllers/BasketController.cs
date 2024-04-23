using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.Repositories.Contract;

namespace Route.Talabat.APIs.Controllers
{

    public class BasketController : _BaseController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            if (basket is not null)
                return Ok(basket);
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasket basket)
        {
            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            if (createdOrUpdatedBasket is not null)
                return Ok(createdOrUpdatedBasket);
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
        }

        [HttpDelete]
        public async Task<bool> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
