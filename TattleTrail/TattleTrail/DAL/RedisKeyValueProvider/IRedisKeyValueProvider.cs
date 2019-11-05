﻿using StackExchange.Redis;
using System.Collections.Generic;

namespace TattleTrail.DAL.RedisKeyValueProvider {
    public interface IRedisKeyValueProvider {
        Dictionary<RedisKey, RedisValue> GetKeyValues(IServer server, IDatabase database);
    }
}