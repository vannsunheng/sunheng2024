using System.Text.Json;
using Core.Entities;
using Core.Interface;
using StackExchange.Redis;

namespace Infrastructure.Data.SeedData
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database=redis.GetDatabase();
        }

       public async Task<bool> DeleteBasketAsync(string basketId)
        {
           return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string BasketID)
        {
            var data=await _database.StringGetAsync(BasketID);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var create=await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));
            if(!create) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}