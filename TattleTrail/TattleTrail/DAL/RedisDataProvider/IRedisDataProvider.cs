using StackExchange.Redis;
using System.Collections.Generic;

namespace TattleTrail.DAL.RedisDataProvider {
    public interface IRedisDataProvider {
        Dictionary<RedisKey, RedisValue> GetKeyValues();


    }
}