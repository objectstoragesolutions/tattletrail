using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class HashEntryExtensions {
        public static MonitorProcess AsMonitorProcess(this HashEntry[] hashEntry, Guid guid) {
            
            HashEntry processName = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.ProcessName));
            HashEntry lifeTime = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.IntervalTime));
            HashEntry subscribers = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.Subscribers));
            HashEntry dateOfCreation = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.DateOfCreation));
            HashEntry lastCheckIn = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.LastCheckIn));

            MonitorProcess result = new MonitorProcess { 
            Id = guid, 
                MonitorDetails = new MonitorDetails { 
                    ProcessName = processName.Value.HasValue ? processName.Value.ToString() : String.Empty,
                    IntervalTime = lifeTime.Value.HasValue ? (int)lifeTime.Value : 0,
                    Subscribers = subscribers.Value.HasValue ? JsonConvert.DeserializeObject<string[]>(subscribers.Value.ToString()) : new string[]{ },
                    DateOfCreation = dateOfCreation.Value.HasValue ? DateTime.Parse(dateOfCreation.Value): DateTime.MinValue,
                    LastCheckIn = lastCheckIn.Value.HasValue ? DateTime.Parse(lastCheckIn.Value) : DateTime.MinValue
                }
            };

            return result;
        }

        public static CheckIn AsCheckInProcess(this HashEntry[] hashEntry, Guid guid) {
            HashEntry monitorId = hashEntry.FirstOrDefault(x => x.Name == nameof(CheckIn.MonitorId));

            return new CheckIn { CheckInId = guid, MonitorId = monitorId.Value.HasValue ? Guid.Parse(monitorId.Value) : Guid.Empty };
        }
    }
}
