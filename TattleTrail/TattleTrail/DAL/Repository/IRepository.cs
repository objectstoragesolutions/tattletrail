using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using TattleTrail.Models;

namespace TattleTrail.DAL {
    public interface IRepository {
        Task<HashEntry[]> GetAllMonitors();
        Task<RedisValue> GetUserAsync(String userId);
        Task CreateUserAsync(User user);
        Task AddMonitorAsync(MonitorProcess monitor);
        Task DeleteMonitorAsync(Guid monitorId);
        Task<MonitorProcess> GetMonitorAsync(Guid monitorId);
    }
}
