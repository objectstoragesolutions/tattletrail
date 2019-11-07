using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerInfoProvider;
using TattleTrail.Models;
using TattleTrail.Infrastructure.Extensions;

namespace TattleTrail.DAL.RedisDataProvider {
    public class RedisDataProvider : IRedisDataProvider {

        private const int INDEX_OF_FIRST_ENDPOINT = 0;
        private readonly IRedisServerProvider _serverProvider;
        public RedisDataProvider(IRedisServerProvider serverProvider) {
            _serverProvider = serverProvider ?? throw new ArgumentNullException(nameof(RedisDataProvider));
            String endPoint = _serverProvider.GetCertainEndpoint(INDEX_OF_FIRST_ENDPOINT);
            Server = serverProvider.GetServer(endPoint) ?? throw new ArgumentNullException(nameof(RedisDataProvider)); ;
            Database = serverProvider.GetDatabase() ?? throw new ArgumentNullException(nameof(RedisDataProvider)); ;
        }

        public IServer Server { get; }
        public IDatabase Database { get; }

        public async Task<RedisValue> GetUserAsync(Guid userId) {
            return await Database.HashGetAsync(userId.ToByteArray(),"");
        }

        public async Task CreateUserAsync(User user) {
             await Database.HashSetAsync(Guid.NewGuid().ToByteArray(), user.ConvertUserToHashEntry());
        }
        public Dictionary<RedisKey, RedisValue> GetKeyValues() {
            Dictionary<RedisKey,RedisValue> redisKeyValues = new Dictionary<RedisKey, RedisValue>();

            foreach (RedisKey key in Server.Keys(pattern: "*")) {
                redisKeyValues.Add(key, Database.StringGet(key));
            }
            return redisKeyValues;
        }

    }
}
