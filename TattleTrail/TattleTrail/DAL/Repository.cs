﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisKeyValueProvider;
using TattleTrail.DAL.RedisServerInfoProvider;
using TattleTrail.Models;

namespace TattleTrail.DAL {
    public class Repository : IRepository<MonitorModel> {
        private const int INDEX_OF_FIRST_ENDPOINT = 0;
        private readonly IRedisServerProvider _serverProvider;
        private readonly IRedisKeyValueProvider _keyValueProvider;

        public Repository(IRedisServerProvider serverProvider, IRedisKeyValueProvider keyValueProvider) {
            _serverProvider = serverProvider ?? throw new ArgumentNullException(nameof(serverProvider)); ;
            _keyValueProvider = keyValueProvider ?? throw new ArgumentNullException(nameof(serverProvider)); ;
        }
        public async Task<ActionResult<Boolean>> AddMonitorAsync(MonitorModel monitor) {
            return await _serverProvider.GetDatabase().StringSetAsync(Guid.NewGuid().ToByteArray(), JsonConvert.SerializeObject(monitor));
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
