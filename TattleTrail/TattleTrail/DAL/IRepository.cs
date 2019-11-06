using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TattleTrail.DAL {
    public interface IRepository<TEntity> where TEntity:class {

        Task<ActionResult<Dictionary<RedisKey, RedisValue>>> GetAllMonitorsAsync();
        Task<ActionResult<RedisValue?>> GetMonitorAsync(String id);
        Task<ActionResult<Boolean>> AddMonitorAsync(TEntity monitor);
        Task<ActionResult<Boolean>> DeleteMonitorAsync(String monitorId);

    }
}
