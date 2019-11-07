
using StackExchange.Redis;
using System;
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

        public async Task<HashEntry[]> GetAllMonitors() {
            return await _dataProvider.Database.HashGetAllAsync("*");
        }

        public async Task AddMonitorAsync(MonitorProcess monitor) {
            await _dataProvider.Database.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry());
        }

        public async Task DeleteMonitorAsync(Guid monitorId) {
            await _dataProvider.Database.KeyDeleteAsync(monitorId.ToByteArray());
        }

        public async Task<HashEntry[]> GetMonitorAsync(Guid monitorId) {
            return await _dataProvider.Database.HashGetAllAsync(monitorId.ToString());
        }

        public async Task<RedisValue> GetUserAsync(String userId) {
            return await _dataProvider.Database.HashGetAsync(userId, "*");
        }

        public async Task CreateUserAsync(User user) {
            await _dataProvider.Database.HashSetAsync(Guid.NewGuid().ToByteArray(), user.ConvertUserToHashEntry());
        }
    }
}
