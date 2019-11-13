using Newtonsoft.Json;
using StackExchange.Redis;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class MonitorProcessModelExtensions {
        public static HashEntry[] ConvertMonitorToHashEntry(this MonitorProcess monitor) {
            var processName = new HashEntry(nameof(monitor.MonitorDetails.ProcessName), monitor.MonitorDetails.ProcessName);
            var lifeTime = new HashEntry(nameof(monitor.MonitorDetails.IntervalTime), monitor.MonitorDetails.IntervalTime);
            var subscribers = new HashEntry(nameof(monitor.MonitorDetails.Subscribers), JsonConvert.SerializeObject(monitor.MonitorDetails.Subscribers));
            var dateOfCreation = new HashEntry(nameof(monitor.MonitorDetails.DateOfCreation), monitor.MonitorDetails.DateOfCreation.ToString());
            var checkeInLastTime = new HashEntry(nameof(monitor.MonitorDetails.LastCheckIn), monitor.MonitorDetails.LastCheckIn.ToString());

            return new HashEntry[] { processName, lifeTime, subscribers, dateOfCreation, checkeInLastTime };

        }
    }
}
