using System.Text.Json.Serialization;
using Basket.API.Common.Dependency;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository , IScopedDependency
    {
        private readonly IDistributedCache _redis;

        public BasketRepository(IDistributedCache redis)
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(_redis));
        }
        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redis.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redis.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string username)
        {
            await _redis.RemoveAsync(username);
        }
    }
}
