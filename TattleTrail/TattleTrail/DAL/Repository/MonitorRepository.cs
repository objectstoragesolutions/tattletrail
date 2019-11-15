using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MonitorRepository> _logger;

        public MonitorRepository(IRedisServerProvider dataProvider, ILogger<MonitorRepository> logger) {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(RedisServerProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<MonitorRepository>));
        }

        public async Task CreateAsync(MonitorProcess monitor) {
            try {
                await _dataProvider.Database.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry());
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorRepository -> CreateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid monitorId) {
            try {
                await _dataProvider.Database.KeyDeleteAsync(monitorId.ToString());
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorRepository -> DeleteAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<HashSet<MonitorProcess>> GetAllAsync() {
            try {
                HashSet<MonitorProcess> monitors = new HashSet<MonitorProcess>();

                var hashKeys = GetAllMonitorsKeys();

                foreach (var key in hashKeys) {
                    if (Guid.TryParse(key.ToString(), out _)) {
                        monitors.Add(await GetAsync(key));
                    }
                }

                return monitors;
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorRepository -> GetAllAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<MonitorProcess> GetAsync(RedisKey monitorId) {
            try {
                HashEntry[] monitorData = await GetHashEntryArrayByKey(monitorId);
                if (monitorData.Length.Equals(0)) {
                    return new MonitorProcess();
                }
                return monitorData.AsMonitorProcess(monitorId);
            } catch(Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorRepository -> GetAsync: {ex.Message}");
                throw;
            }
        }

        private IEnumerable<RedisKey> GetAllKeys() {
            try {
                return _dataProvider.Server.Keys(pattern: "*");
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorRepository -> GetAllKeys: {ex.Message}");
                throw;
            }
        }

        private HashSet<RedisKey> GetAllMonitorsKeys() {
            try { 
            HashSet<RedisKey> hashKeys = new HashSet<RedisKey>();
            var allHashKeys = GetAllKeys();
            foreach (var key in allHashKeys) {
                hashKeys.Add(key);
            }
            return hashKeys;
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorRepository -> GetAllMonitorsKeys: {ex.Message}");
                throw;
            }
        }

        private async Task<HashEntry[]> GetHashEntryArrayByKey(RedisKey redisKey) {
            try {
                return await _dataProvider.Database.HashGetAllAsync(redisKey);
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside MonitorRepository -> GetHashEntryArrayByKey: {ex.Message}");
                throw;
            }
        }
    }
}
