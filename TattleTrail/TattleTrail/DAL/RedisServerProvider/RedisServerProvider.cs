using StackExchange.Redis;
using System.Net;
using System;

namespace TattleTrail.DAL.RedisServerProvider {
    public class RedisServerProvider : IRedisServerProvider {
        private const int INDEX_OF_FIRST_ENDPOINT = 0;
        private readonly IConnectionMultiplexer _multiplexer;
        public RedisServerProvider(IConnectionMultiplexer multiplexer) {
            _multiplexer = multiplexer ?? throw new ArgumentNullException(nameof(multiplexer));
            String endPoint = GetCertainEndpoint(INDEX_OF_FIRST_ENDPOINT);
            Server = _multiplexer.GetServer(endPoint) ?? throw new ArgumentNullException(nameof(RedisServerProvider));
            Database = _multiplexer.GetDatabase() ?? throw new ArgumentNullException(nameof(RedisServerProvider));
        }

        public IServer Server { get; }
        public IDatabase Database { get; }

        private String GetCertainEndpoint(int id) {
            var allEndpoints = _multiplexer.GetEndPoints();
            if (IndexOfArrayExists(allEndpoints, id)) {
                var endPoint = allEndpoints[id].ToString();
                return endPoint.Substring(endPoint.IndexOf("/") + 1);
            }
            return String.Empty;
        }

        private Boolean IndexOfArrayExists(EndPoint[] endpoints, int indexId) {
            return endpoints.Length > indexId;
        }
    }
}
