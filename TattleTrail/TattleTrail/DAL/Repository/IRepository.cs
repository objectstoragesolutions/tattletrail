using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
using TattleTrail.Models;

namespace TattleTrail.DAL.Repository {
    public interface IRepository {
        Task CheckInMonitorAsync(MonitorProcess monitor);
        Task<bool> CreateMonitorAsync(MonitorProcess monitor);
        Task DeleteMonitorAsync(Guid monitorId);
        Task<List<CheckIn>> GetAllCheckIns();
        Task<HashSet<MonitorProcess>> GetAllMonitors();
        Task<CheckIn> GetCheckIn(RedisKey checkInId);
        IEnumerable<RedisKey> GetCheckInKeys();
        HashSet<Guid> GetHashKeys(string searchPattern = "*");
        IEnumerable<RedisKey> GetHashKeysByPattern(string searchPattern);
        Task<MonitorProcess> GetMonitorAsync(Guid monitorId);
    }
}