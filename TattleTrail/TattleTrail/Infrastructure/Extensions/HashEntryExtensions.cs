using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class HashEntryExtensions {
        public static MonitorProcess AsMonitorProcess(this HashEntry[] hashEntry, Guid guid) {
            MonitorProcess result = new MonitorProcess();
            HashEntry processName = hashEntry.FirstOrDefault(x => x.Name == nameof(result.ProcessName));
            HashEntry lifeTime = hashEntry.FirstOrDefault(x => x.Name == nameof(result.LifeTime));
            HashEntry subscribers = hashEntry.FirstOrDefault(x => x.Name == nameof(result.Subscribers));

            result.Id = guid;
            result.ProcessName = processName.Value.HasValue ? processName.Value.ToString() : String.Empty;
            result.LifeTime = lifeTime.Value.HasValue ? (int)lifeTime.Value : 0;
            result.Subscribers = subscribers.Value.HasValue ? JsonConvert.DeserializeObject<string[]>(subscribers.Value.ToString()) : new string[]{ };

            return result;
        }
    }
}
