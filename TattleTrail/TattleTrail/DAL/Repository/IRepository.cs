using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.Models;

namespace TattleTrail.DAL {
    public interface IRepository {
        Task<HashSet<MonitorProcess>> GetAllMonitors();
        HashSet<Guid> GetHashKeys(String searchPattern);
        Task<Boolean> CreateMonitorAsync(MonitorProcess monitor);
        Task DeleteMonitorAsync(Guid monitorId);
        Task<MonitorProcess> GetMonitorAsync(Guid monitorId);

        Task CheckInMonitorAsync(MonitorProcess monitor);

        Task<List<CheckIn>> GetAllCheckIns();
        Task<CheckIn> GetCheckIn(RedisKey checkInId);
    }
}
