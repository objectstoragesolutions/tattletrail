using StackExchange.Redis;
using System.Collections.Generic;

namespace TattleTrail.DAL.RedisKeyValueProvider {
    public class RedisKeyValueProvider : IRedisKeyValueProvider {
        public Dictionary<RedisKey, RedisValue> GetKeyValues(IServer server, IDatabase database) {
            Dictionary<RedisKey,RedisValue> redisKeyValues = new Dictionary<RedisKey, RedisValue>();

            foreach (RedisKey key in server.Keys(pattern: "*")) {
                redisKeyValues.Add(key, database.StringGet(key));
            }
            return redisKeyValues;
        }
    }
}
