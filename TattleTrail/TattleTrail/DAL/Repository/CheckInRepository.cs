using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.DAL.Repository {
    public class CheckInRepository : ICheckInRepository<CheckIn> {

        private readonly IRedisServerProvider _dataProvider;
        private readonly ICheckInModelFactory _checkInModelFactory;
        private readonly ILogger<CheckInRepository> _logger;

        public CheckInRepository(IRedisServerProvider dataProvider, ICheckInModelFactory checkInFactory, ILogger<CheckInRepository> logger) {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(IRedisServerProvider));
            _checkInModelFactory = checkInFactory ?? throw new ArgumentNullException(nameof(CheckInModelFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<CheckInRepository>));
        }
        public async Task CreateAsync(Guid id, Int32 interval) {
            try {
                var checkIn = _checkInModelFactory.Create(id);
                var data = new HashEntry(nameof(CheckIn.MonitorId), checkIn.MonitorId.ToString());

                await _dataProvider.Database.HashSetAsync(checkIn.CreateKeyString(), new HashEntry[] { data });

                await _dataProvider.Database.KeyExpireAsync(checkIn.CreateKeyString(), TimeSpan.FromSeconds(interval));
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CheckInRepository -> CreateAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<HashSet<CheckIn>> GetAllAsync() {
            try {
                HashSet<CheckIn> checkIns = new HashSet<CheckIn>();
                var hashKeys = GetAllCheckInKeys();

                foreach (var key in hashKeys) {
                    checkIns.Add(await GetAsync(key));
                }

                return checkIns;
            } catch (Exception ex) { 
                _logger.LogError($"Something went wrong inside CheckInRepository -> GetAllAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<CheckIn> GetAsync(RedisKey key) {
            try {
                HashEntry[] checkInData = await GetHashEntryArrayByKey(key);

                if (checkInData.Length.Equals(0)) {
                    return new CheckIn();
                }

                return checkInData.AsCheckInProcess(key);
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CheckInRepository -> GetAsync: {ex.Message}");
                throw;
            }
        }

        private async Task<HashEntry[]> GetHashEntryArrayByKey(RedisKey redisKey) {
            try {
                return await _dataProvider.Database.HashGetAllAsync(redisKey);
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CheckInRepository -> GetHashEntryArrayByKey: {ex.Message}");
                throw;
            }
        }

        private IEnumerable<RedisKey> GetAllCheckInKeys() {
            try {
                return _dataProvider.Server.Keys(pattern: "checkinid:*");
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CheckInRepository -> GetAllCheckInKeys: {ex.Message}");
                throw;
            }
        }
    }
}
