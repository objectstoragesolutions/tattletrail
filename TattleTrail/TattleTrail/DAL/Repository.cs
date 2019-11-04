using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using TattleTrail.Models;

namespace TattleTrail.DAL
{
    public class Repository : IRepository<MonitorModel> {
        private IConnectionMultiplexer _connectionMultiplexer;
        private IDatabase database;

        public Repository(IConnectionMultiplexer multiplexer) {
            _connectionMultiplexer = multiplexer;
            if (_connectionMultiplexer != null) {
                database = _connectionMultiplexer.GetDatabase();
            }
        }
        public async Task<ActionResult<Boolean>> AddMonitorAsync(MonitorModel monitor, TimeSpan? timeSpan) {
            if (database != null) {
               return await database.StringSetAsync(monitor.Id, monitor.MonitorName, timeSpan);
            }
            return false;
        }

        public async Task<ActionResult<Boolean>> DeleteMonitorAsync(String monitorId) {
            if (database != null) {
                return await database.KeyDeleteAsync(monitorId);
            }

            return false;
        }

        public async Task<ActionResult<RedisValue[]>> GetAllMonitorsAsync() {
            var result = new RedisValue[] { };
            if (database != null) {
                //TODO: shoul be implemented later.
                //result = false;
            }

            return result;
        }

        public async Task<ActionResult<RedisValue?>> GetMonitorAsync(String id) {
            if (database != null) {
                return await database.StringGetAsync(id);
            }

            return null;
        }
    }
}
