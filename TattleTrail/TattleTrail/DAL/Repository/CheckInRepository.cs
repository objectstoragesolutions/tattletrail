using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.DAL.Repository {
    public class CheckInRepository : ICheckInRepository<CheckIn> {

        private readonly IRedisServerProvider _dataProvider;
        private readonly ICheckInModelFactory _checkInModelFactory;

        public CheckInRepository(IRedisServerProvider dataProvider, ICheckInModelFactory checkInFactory) {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(RedisServerProvider));
            _checkInModelFactory = checkInFactory ?? throw new ArgumentNullException(nameof(CheckInModelFactory));
        }
        public async Task CreateAsync(Guid id, Int32 interval) {
            var checkIn = _checkInModelFactory.Create(id);
            var data = new HashEntry(nameof(CheckIn.MonitorId), checkIn.MonitorId.ToString());

            await _dataProvider.Database.HashSetAsync(checkIn.CreateKeyString(), new HashEntry[] { data });

            await _dataProvider.Database.KeyExpireAsync(checkIn.CreateKeyString(), TimeSpan.FromSeconds(interval));
        }

        public async Task<HashSet<CheckIn>> GetAllAsync() {
            HashSet<CheckIn> checkIns = new HashSet<CheckIn>();
            var hashKeys = GetAllCheckInKeys("checkinid:*");
            foreach (var key in hashKeys) {
                checkIns.Add(await GetAsync(key));
            }
            return checkIns;
        }

        public async Task<CheckIn> GetAsync(RedisKey key) {
            HashEntry[] checkInData = await GetHashEntryArrayByKey(key);
            if (checkInData.Length.Equals(0)) {
                return new CheckIn();
            }
            return checkInData.AsCheckInProcess(key);
        }

        private async Task<HashEntry[]> GetHashEntryArrayByKey(RedisKey redisKey) {
            return await _dataProvider.Database.HashGetAllAsync(redisKey);
        }

        private IEnumerable<RedisKey> GetAllCheckInKeys(String pattern) {
            return _dataProvider.Server.Keys(pattern: pattern);
        }
    }
}
