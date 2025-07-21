using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Real_ChatApi.Infrastructure.Redis
{
    public class MessageCacheService
    {
        private readonly StackExchange.Redis.IDatabase _redisDb;

        public MessageCacheService(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }

        public async Task CacheMessageAsync(Guid messageId, string content)
        {
            await _redisDb.StringSetAsync($"msg:{messageId}", content, TimeSpan.FromMinutes(5));
        }

        public async Task<string?> GetCachedMessageAsync(Guid messageId)
        {
            return await _redisDb.StringGetAsync($"msg:{messageId}");
        }
    }
}
