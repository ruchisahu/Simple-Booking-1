using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace TicketsCartAPI.Domain
{
    public class RedisCartRepository : ICartRepository
    {
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase database;

        public RedisCartRepository(ConnectionMultiplexer redis)
        {
            this.redis = redis;
            this.database = redis.GetDatabase();
        }

        public async Task<bool> DeleteCartAsync(string id)
        {
            return await database.KeyDeleteAsync(id);
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            var data = await database.StringGetAsync(cartId);
            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<Cart>(data);
        }

        public async Task<Cart> UpdateCartAsync(Cart basket)
        {
            var created = await database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if (!created)
            {
                return null;
            }

            return await GetCartAsync(basket.BuyerId);
        }
    }
}
