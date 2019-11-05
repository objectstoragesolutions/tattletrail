using System.Net;
using StackExchange.Redis;

namespace TattleTrail.DAL.RedisServerInfoProvider {
    public interface IRedisServerProvider {
        EndPoint GetCertainEndpoint(int id);
        IDatabase GetDatabase();
        IServer GetServer(string host);
    }
}