﻿using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerInfoProvider;
using TattleTrail.DAL.RedisKeyValueProvider;
using TattleTrail.Models;

namespace TattleTrail.DAL {
    public class Repository : IRepository<MonitorModel> {
        private const int INDEX_OF_FIRST_ENDPOINT = 0;
        private readonly IRedisServerProvider _serverProvider;
        private readonly IRedisKeyValueProvider _keyValueProvider;

        public Repository(IRedisServerProvider serverProvider, IRedisKeyValueProvider keyValueProvider) {
            if (serverProvider is null) {
                throw new ArgumentNullException(nameof(serverProvider));
            }
            if (keyValueProvider is null) {
                throw new ArgumentNullException(nameof(keyValueProvider));
            }
            _serverProvider = serverProvider;
            _keyValueProvider = keyValueProvider;
        }
        public async Task<ActionResult<Boolean>> AddMonitorAsync(MonitorModel monitor, TimeSpan? timeSpan) {
            return await _serverProvider.GetDatabase().StringSetAsync(monitor.Id, monitor.MonitorName, timeSpan);
        }

        public async Task<ActionResult<Boolean>> DeleteMonitorAsync(String monitorId) {
            return await _serverProvider.GetDatabase().KeyDeleteAsync(monitorId);
        }

        public async Task<ActionResult<Dictionary<RedisKey, RedisValue>>> GetAllMonitorsAsync() {

            String endPoint = _serverProvider.GetCertainEndpoint(INDEX_OF_FIRST_ENDPOINT);

            if (String.IsNullOrEmpty(endPoint)) {
                return new Dictionary<RedisKey, RedisValue>();
            }

            IServer server = _serverProvider.GetServer(endPoint);

            return await Task.Run(() => _keyValueProvider.GetKeyValues(server, _serverProvider.GetDatabase()));
        }

        public async Task<ActionResult<RedisValue?>> GetMonitorAsync(String id) {
            return await _serverProvider.GetDatabase().StringGetAsync(id);
        }
    }
}
