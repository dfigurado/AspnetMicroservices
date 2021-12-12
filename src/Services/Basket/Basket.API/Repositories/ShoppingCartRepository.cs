using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ShoppingCart.API.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IDistributedCache _redisCache;

        public ShoppingCartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task DeleteShoppingCart(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<Entities.ShoppingCart> GetShoppingCart(string userName)
        {
            var cart = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(cart))
                return null;

            return JsonConvert.DeserializeObject<Entities.ShoppingCart>(cart);
        }

        public async Task<Entities.ShoppingCart> UpdateShoppingCart(Entities.ShoppingCart shoppingCart)
        {
            await _redisCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));

            return await GetShoppingCart(shoppingCart.UserName);
        }
    }
}