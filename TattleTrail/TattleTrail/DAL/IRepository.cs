using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace TattleTrail.DAL {
    public interface IRepository<TEntity> where TEntity:class {

        Task<ActionResult<RedisValue[]>> GetAllMonitorsAsync();
        Task<ActionResult<RedisValue?>> GetMonitorAsync(String id);
        Task<ActionResult<Boolean>> AddMonitorAsync(TEntity monitor, TimeSpan? timeSpan);
        Task<ActionResult<Boolean>> DeleteMonitorAsync(String monitorId);

    }
}
