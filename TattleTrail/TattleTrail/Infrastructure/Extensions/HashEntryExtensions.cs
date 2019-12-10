using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class HashEntryExtensions {
        public static MonitorProcess AsMonitorProcess(this HashEntry[] hashEntry, RedisKey monitorId) {
            
            HashEntry processName = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.ProcessName));
            HashEntry lifeTime = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.IntervalTime));
            HashEntry subscribers = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.Subscribers));
            HashEntry dateOfCreation = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.DateOfCreation));
            HashEntry lastCheckIn = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.LastCheckIn));
            HashEntry isDown = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.IsDown));

            try {
                MonitorProcess result = new MonitorProcess {
                    Id = Guid.Parse(monitorId.ToString()),
                    MonitorDetails = new MonitorDetails {
                        ProcessName = processName.Value.HasValue ? Convert.ToString(processName.Value) : String.Empty,
                        IntervalTime = lifeTime.Value.HasValue ? Convert.ToInt32(lifeTime.Value) : 0,
                        Subscribers = subscribers.Value.HasValue ? JsonConvert.DeserializeObject<string[]>(Convert.ToString(subscribers.Value)) : new string[] { },
                        DateOfCreation = dateOfCreation.Value.HasValue ? JsonConvert.DeserializeObject<DateTime>(dateOfCreation.Value) : DateTime.MinValue,
                        LastCheckIn = lastCheckIn.Value.HasValue ? JsonConvert.DeserializeObject<DateTime>(lastCheckIn.Value) : DateTime.MinValue,
                        IsDown = isDown.Value.HasValue ? Convert.ToBoolean(isDown.Value) : false
                    }
                };
                return result;
            } catch {
                return new MonitorProcess();
            }
        }

        public static CheckIn AsCheckInProcess(this HashEntry[] hashEntry, RedisKey checkedInKey) {
            try {
                HashEntry monitorId = hashEntry.FirstOrDefault(x => x.Name == nameof(CheckIn.MonitorId));
                var key = checkedInKey.ToString();
                var croppedKey = key.Substring(key.LastIndexOf(":") + 1);
                bool isValidGuid = Guid.TryParse(croppedKey, out var checkInId);
                if (isValidGuid) {
                    return new CheckIn { CheckInId = checkInId, MonitorId = monitorId.Value.HasValue ? Guid.Parse(monitorId.Value) : Guid.Empty };
                }
                return new CheckIn();
            } catch {
                return new CheckIn();
            }
            
        }
    }
}
