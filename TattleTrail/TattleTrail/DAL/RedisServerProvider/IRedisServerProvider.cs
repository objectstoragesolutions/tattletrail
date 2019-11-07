using StackExchange.Redis;

namespace TattleTrail.DAL.RedisServerProvider {
    public interface IRedisServerProvider {
        IDatabase Database { get; }
        IServer Server { get; }
    }
}