using StackExchange.Redis;
using System;
using System.Net;

namespace TattleTrail.DAL.RedisServerInfoProvider {
    public class RedisServerProvider : IRedisServerProvider {
        private readonly IConnectionMultiplexer _multiplexer;

        public RedisServerProvider(IConnectionMultiplexer multiplexer) {
            _multiplexer = multiplexer ?? throw new ArgumentNullException(nameof(multiplexer)); ;
        }

        public IDatabase GetDatabase() {
            return _multiplexer.GetDatabase();
        }

        public IServer GetServer(String host) {
            return _multiplexer.GetServer(host);
        }

        public String GetCertainEndpoint(int id) {
            var allEndpoints = _multiplexer.GetEndPoints();
            if (IndexOfArrayExists(allEndpoints, id)) {
                return allEndpoints[id].ToString();
            }
            return String.Empty;
        }

        private Boolean IndexOfArrayExists(EndPoint[] endpoints, int indexId) {
            return endpoints.Length > indexId;
        }
    }
}
