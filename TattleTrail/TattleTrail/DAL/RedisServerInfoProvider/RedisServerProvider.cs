using StackExchange.Redis;
using System;
using System.Net;

namespace TattleTrail.DAL.RedisServerInfoProvider {
    public class RedisServerProvider : IRedisServerProvider {
        private readonly IConnectionMultiplexer _multiplexer;

        public RedisServerProvider(IConnectionMultiplexer multiplexer) {
            if (multiplexer is null) {
                throw new ArgumentNullException(nameof(multiplexer));
            }
            _multiplexer = multiplexer;
        }

        public IDatabase GetDatabase() {
            return _multiplexer.GetDatabase();
        }

        public IServer GetServer(String host) {
            return _multiplexer.GetServer(host);
        }

        public EndPoint? GetCertainEndpoint(int id) {
            var allEndpoints = _multiplexer.GetEndPoints();
            if (IndexOfArrayExists(allEndpoints, id)) {
                return allEndpoints[id];
            }
            return null;
        }

        private Boolean IndexOfArrayExists(EndPoint[] endpoints, int indexId) {
            return endpoints.Length > indexId;
        }
    }
}
