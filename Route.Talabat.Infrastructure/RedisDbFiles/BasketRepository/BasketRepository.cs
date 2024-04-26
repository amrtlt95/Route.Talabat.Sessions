using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.Repositories.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure.RedisDbFiles.BasketRepository
{
    public class BasketRepository : IBasketRepository
    {
        private IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);

            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var convertedBasket = JsonSerializer.Serialize(basket);
            var updatedOrCreatedBasket = await _database.StringSetAsync(basket.Id, convertedBasket, TimeSpan.FromDays(30));

            return updatedOrCreatedBasket ? await GetBasketAsync(basket.Id) : null!;
        }
    }
}
