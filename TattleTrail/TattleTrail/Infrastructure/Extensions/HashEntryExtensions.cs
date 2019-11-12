using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Linq;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class HashEntryExtensions {
        public static MonitorProcess AsMonitorProcess(this HashEntry[] hashEntry, Guid guid) {
            
            HashEntry processName = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.ProcessName));
            HashEntry lifeTime = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.LifeTime));
            HashEntry subscribers = hashEntry.FirstOrDefault(x => x.Name == nameof(MonitorDetails.Subscribers));

            MonitorProcess result = new MonitorProcess { 
            Id = guid, 
                MonitorDetails = new MonitorDetails { 
                    ProcessName = processName.Value.HasValue ? processName.Value.ToString() : String.Empty,
                    LifeTime = lifeTime.Value.HasValue ? (int)lifeTime.Value : 0,
                    Subscribers = subscribers.Value.HasValue ? JsonConvert.DeserializeObject<string[]>(subscribers.Value.ToString()) : new string[]{ }
                }
            };

            return result;
        }
    }
}
