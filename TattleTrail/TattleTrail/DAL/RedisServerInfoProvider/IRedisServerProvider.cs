using System;
using StackExchange.Redis;

namespace TattleTrail.DAL.RedisServerInfoProvider {
    public interface IRedisServerProvider {
        String GetCertainEndpoint(int id);
        IDatabase GetDatabase();
        IServer GetServer(string host);
    }
}