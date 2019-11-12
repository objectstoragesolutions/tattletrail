using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;

namespace TattleTrail.DAL.Repository {
    public class Repository : IRepository {
        private readonly IRedisServerProvider _dataProvider;

        public Repository(IRedisServerProvider dataProvider) {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public async Task<HashSet<MonitorProcess>> GetAllMonitors() {
            HashSet<MonitorProcess> monitors = new HashSet<MonitorProcess>();

            var hashKeys = GetAllHashKeysAsync();
            
            foreach (var key in hashKeys) {
                monitors.Add(await GetMonitorAsync(key));
            }

            return monitors;
        }

        public HashSet<Guid> GetAllHashKeysAsync() {
            HashSet<Guid> hashKeys = new HashSet<Guid>();
            IEnumerable<RedisKey> allHashKeys = _dataProvider.Server.Keys(pattern: "*");
            foreach (var key in allHashKeys) {
                bool isValidGuid = Guid.TryParse(key, out var guidOutput);
                if (isValidGuid) {
                    hashKeys.Add(guidOutput);
                }
            }
            return hashKeys;
        }

        public async Task<Boolean> CreateMonitorAsync(MonitorProcess monitor) {
            await _dataProvider.Database.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry());
            return await _dataProvider.Database.KeyExpireAsync(monitor.Id.ToString(), TimeSpan.FromSeconds(monitor.MonitorDetails.LifeTime));
        }

        public async Task DeleteMonitorAsync(Guid monitorId) {
            await _dataProvider.Database.KeyDeleteAsync(monitorId.ToString());
        }

        public async Task<MonitorProcess> GetMonitorAsync(Guid monitorId) {
            HashEntry[] monitorData = await GetHashEntryArrayByKey(monitorId.ToString());
            if (monitorData.Length.Equals(0)) {
                return new MonitorProcess();
            }
            return monitorData.AsMonitorProcess(monitorId);
        }

        public async Task<RedisValue> GetUserAsync(String userId) {
            return await _dataProvider.Database.HashGetAsync(userId, "*");
        }

        public async Task CreateUserAsync(User user) {
            await _dataProvider.Database.HashSetAsync(Guid.NewGuid().ToString(), user.ConvertUserToHashEntry());
        }

        private async Task<HashEntry[]> GetHashEntryArrayByKey(RedisKey redisKey) {
            return await _dataProvider.Database.HashGetAllAsync(redisKey);
        }
    }
}
