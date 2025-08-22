using Draftli.Shared.Interfaces;
using StackExchange.Redis;

namespace Draftli.Infra.Redis;
public class RedisDocumentRepository : IDocumentRepository
{
    private readonly IConnectionMultiplexer redis;
    public RedisDocumentRepository(IConnectionMultiplexer redis)
    {
        this.redis = redis;
    }

    public async Task<long> SaveContentAsync(Guid documentId, string content)
    {
        IDatabase db = this.redis.GetDatabase();

        await db.StringSetAsync($"doc:{documentId}:content", content);

        return await db.StringIncrementAsync($"doc:{documentId}:version");
    }
}