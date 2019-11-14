using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;

namespace TattleTrail.DAL.Repository {
    public class MonitorRepository : IMonitorRepository<MonitorProcess> {

        private readonly IRedisServerProvider _dataProvider;

        public MonitorRepository(IRedisServerProvider dataProvider) {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(RedisServerProvider));
        }

        public async Task CreateAsync(MonitorProcess monitor) {
            await _dataProvider.Database.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry());
        }

        public async Task DeleteAsync(Guid monitorId) {
            await _dataProvider.Database.KeyDeleteAsync(monitorId.ToString());
        }

        public async Task<HashSet<MonitorProcess>> GetAllAsync() {
            HashSet<MonitorProcess> monitors = new HashSet<MonitorProcess>();

            var hashKeys = GetAllMonitorsKeys();

            foreach (var key in hashKeys) {
                if (Guid.TryParse(key.ToString(), out _)) {
                    monitors.Add(await GetAsync(key));
                }
            }

            return monitors;
        }

        public async Task<MonitorProcess> GetAsync(RedisKey monitorId) {
            HashEntry[] monitorData = await GetHashEntryArrayByKey(monitorId);
            if (monitorData.Length.Equals(0)) {
                return new MonitorProcess();
            }
            return monitorData.AsMonitorProcess(monitorId);
        }

        private HashSet<RedisKey> GetAllMonitorsKeys() {
            HashSet<RedisKey> hashKeys = new HashSet<RedisKey>();
            var allHashKeys = GetAllKeys("*");
            foreach (var key in allHashKeys) {
                hashKeys.Add(key);
            }
            return hashKeys;
        }

        private IEnumerable<RedisKey> GetAllKeys(String pattern) {
            return _dataProvider.Server.Keys(pattern: pattern);
        }

        private async Task<HashEntry[]> GetHashEntryArrayByKey(RedisKey redisKey) {
            return await _dataProvider.Database.HashGetAllAsync(redisKey);
        }
    }
}
