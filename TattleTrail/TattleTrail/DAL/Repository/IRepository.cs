using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.Models;

namespace TattleTrail.DAL {
    public interface IRepository {
        Task<HashEntry[]> GetAllMonitors();
        Task<RedisValue> GetUserAsync(Guid userId);
        Task CreateUserAsync(User user);
        Task AddMonitorAsync(MonitorProcess monitor);

        Task DeleteMonitorAsync(Guid monitorId);

        Task<HashEntry[]> GetMonitorAsync(Guid monitorId);
    }
}
