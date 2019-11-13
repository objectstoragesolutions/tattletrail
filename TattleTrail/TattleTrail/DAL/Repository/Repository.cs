using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.DAL.Repository {
    public class Repository : IRepository {
        private readonly IRedisServerProvider _dataProvider;
        private readonly ICheckInModelFactory _checkInFactory;

        public Repository(IRedisServerProvider dataProvider, ICheckInModelFactory checkInFactory) {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(RedisServerProvider));
            _checkInFactory = checkInFactory ?? throw new ArgumentNullException(nameof(CheckInModelFactory));
        }

        public async Task<HashSet<MonitorProcess>> GetAllMonitors() {
            HashSet<MonitorProcess> monitors = new HashSet<MonitorProcess>();

            var hashKeys = GetHashKeys();

            foreach (var key in hashKeys) {
                monitors.Add(await GetMonitorAsync(key));
            }

            return monitors;
        }

        public async Task<List<CheckIn>> GetAllCheckIns() {
            List<CheckIn> checkIns = new List<CheckIn>();
            var hashKeys = GetCheckInKeys();
            foreach (var key in hashKeys) {
                checkIns.Add(await GetCheckIn(key));
            }
            return checkIns;
        }

        public async Task<CheckIn> GetCheckIn(RedisKey checkInId) {

            HashEntry[] checkInData = await GetHashEntryArrayByKey(checkInId);
            if (checkInData.Length.Equals(0)) {
                return new CheckIn();
            }
            return checkInData.AsCheckInProcess(checkInId);
        }

        public HashSet<Guid> GetHashKeys(String searchPattern = "*") {
            HashSet<Guid> hashKeys = new HashSet<Guid>();
            var allHashKeys = GetHashKeysByPattern(searchPattern);
            foreach (var key in allHashKeys) {
                bool isValidGuid = Guid.TryParse(key, out var guidOutput);
                if (isValidGuid) {
                    hashKeys.Add(guidOutput);
                }
            }
            return hashKeys;
        }

        public IEnumerable<RedisKey> GetHashKeysByPattern(String searchPattern) {
            return _dataProvider.Server.Keys(pattern: searchPattern);
        }

        public async Task<Boolean> CreateMonitorAsync(MonitorProcess monitor) {
            await _dataProvider.Database.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry());
            //return await _dataProvider.Database.KeyExpireAsync(monitor.Id.ToString(), TimeSpan.FromSeconds(monitor.MonitorDetails.IntervalTime));
            return true;
        }

        public async Task DeleteMonitorAsync(Guid monitorId) {
            await _dataProvider.Database.KeyDeleteAsync(monitorId.ToString());
        }

        public async Task<MonitorProcess> GetMonitorAsync(Guid monitorId) {
            HashEntry[] monitorData = await GetHashEntryArrayByKey(monitorId.ToString());
            if (monitorData.Length.Equals(0)) {
                return new MonitorProcess();
            }
            return monitorData.AsMonitorProcess(monitorId);
        }

        private async Task<HashEntry[]> GetHashEntryArrayByKey(RedisKey redisKey) {
            return await _dataProvider.Database.HashGetAllAsync(redisKey);
        }

        public IEnumerable<RedisKey> GetCheckInKeys() {
            return GetHashKeysByPattern("checkinid:*");
            //HashSet<Guid> checkInKeys = new HashSet<Guid>();
            //foreach (String key in checkInHashKeys) {
            //    var croppedKey = key.Substring(key.IndexOf(":"));
            //    bool isValidGuid = Guid.TryParse(croppedKey, out var checkInId);
            //    if (isValidGuid) {
            //        checkInKeys.Add(checkInId);
            //    }
            //}
            //return checkInKeys;
        }

        public async Task CheckInMonitorAsync(MonitorProcess monitor) {
            var checkIn = _checkInFactory.Create(monitor.Id);
            var data = new HashEntry(nameof(CheckIn.MonitorId), checkIn.MonitorId.ToString());

            //TODO: ADD FACTORY FOR nameof(CheckIn.CheckInId).ToLower() + ":" +  checkIn.CheckInId.ToString()
            await _dataProvider.Database.HashSetAsync(nameof(CheckIn.CheckInId).ToLower() + ":" + checkIn.CheckInId.ToString(), new HashEntry[] { data });

            await _dataProvider.Database.KeyExpireAsync(nameof(CheckIn.CheckInId).ToLower() + ":" + checkIn.CheckInId.ToString(),
                TimeSpan.FromSeconds(monitor.MonitorDetails.IntervalTime));

            monitor.MonitorDetails.LastCheckIn = DateTime.UtcNow;
            await CreateMonitorAsync(monitor);
        }
    }
}
