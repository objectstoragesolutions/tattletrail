using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.Models;

namespace TattleTrail.DAL {
    public interface IRepository {
        Task<HashSet<MonitorProcess>> GetAllMonitors();
        HashSet<Guid> GetAllHashKeysAsync();
        Task<Boolean> CreateMonitorAsync(MonitorProcess monitor);
        Task DeleteMonitorAsync(Guid monitorId);
        Task<MonitorProcess> GetMonitorAsync(Guid monitorId);

        Task CheckInMonitorAsync(MonitorProcess monitor);
    }
}
