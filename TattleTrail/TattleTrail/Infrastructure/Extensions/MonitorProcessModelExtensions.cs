using Newtonsoft.Json;
using StackExchange.Redis;
using TattleTrail.Models;
using Microsoft.AspNetCore.Http;
using System;
using Newtonsoft.Json.Linq;

namespace TattleTrail.Infrastructure.Extensions {
    public static class MonitorProcessModelExtensions {
        public static HashEntry[] ConvertMonitorToHashEntry(this MonitorProcess monitor) {
            var processName = new HashEntry(nameof(monitor.MonitorDetails.ProcessName), monitor.MonitorDetails.ProcessName);
            var lifeTime = new HashEntry(nameof(monitor.MonitorDetails.IntervalTime), monitor.MonitorDetails.IntervalTime);
            var subscribers = new HashEntry(nameof(monitor.MonitorDetails.Subscribers), JsonConvert.SerializeObject(monitor.MonitorDetails.Subscribers));
            var dateOfCreation = new HashEntry(nameof(monitor.MonitorDetails.DateOfCreation), JsonConvert.SerializeObject(monitor.MonitorDetails.DateOfCreation));
            var checkedLastTime = new HashEntry(nameof(monitor.MonitorDetails.LastCheckIn), JsonConvert.SerializeObject(monitor.MonitorDetails.LastCheckIn));
            var isDown = new HashEntry(nameof(monitor.MonitorDetails.IsDown), monitor.MonitorDetails.IsDown.ToString());

            return new HashEntry[] { processName, lifeTime, subscribers, dateOfCreation, checkedLastTime, isDown };
        }

        public static JObject GetResultJson(this MonitorProcess monitor, HostString host, String scheme, PathString path) {
            var pathValue = path.HasValue ? path.Value : String.Empty;
            var hostValue = host.HasValue ? host.Value : String.Empty;
            if (!pathValue.EndsWith("/")) {
                pathValue += "/";
            }
            var checkInUrl = scheme + "://" + hostValue + pathValue + monitor.Id.ToString() + "/checkin";
            var result = new { monitorid = monitor.Id, checkinurl = checkInUrl };
            return JObject.FromObject(result);
        }
    }
}
