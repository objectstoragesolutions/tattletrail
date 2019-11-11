using Newtonsoft.Json;
using StackExchange.Redis;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class MonitorProcessModelExtensions {
        public static HashEntry[] ConvertMonitorToHashEntry(this MonitorProcess monitor) {
            var processName = new HashEntry(nameof(monitor.MonitorDetails.ProcessName), monitor.MonitorDetails.ProcessName);
            var lifeTime = new HashEntry(nameof(monitor.MonitorDetails.LifeTime), monitor.MonitorDetails.LifeTime);
            var subscribers = new HashEntry(nameof(monitor.MonitorDetails.Subscribers), JsonConvert.SerializeObject(monitor.MonitorDetails.Subscribers));

            return new HashEntry[] { processName, lifeTime, subscribers };

        }
    }
}
