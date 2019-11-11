using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class HashEntryExtensions {
        public static MonitorProcess AsMonitorProcess(this HashEntry[] hashEntry, Guid guid) {
            MonitorProcess result = new MonitorProcess();
            HashEntry processName = hashEntry.FirstOrDefault(x => x.Name == nameof(result.MonitorDetails.ProcessName));
            HashEntry lifeTime = hashEntry.FirstOrDefault(x => x.Name == nameof(result.MonitorDetails.LifeTime));
            HashEntry subscribers = hashEntry.FirstOrDefault(x => x.Name == nameof(result.MonitorDetails.Subscribers));

            result.Id = guid;
            result.MonitorDetails.ProcessName = processName.Value.HasValue ? processName.Value.ToString() : String.Empty;
            result.MonitorDetails.LifeTime = lifeTime.Value.HasValue ? (int)lifeTime.Value : 0;
            result.MonitorDetails.Subscribers = subscribers.Value.HasValue ? JsonConvert.DeserializeObject<string[]>(subscribers.Value.ToString()) : new string[]{ };

            return result;
        }
    }
}
