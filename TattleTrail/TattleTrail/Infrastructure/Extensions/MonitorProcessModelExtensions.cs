﻿using StackExchange.Redis;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class MonitorProcessModelExtensions {
        public static HashEntry[] ConvertMonitorToHashEntry(this MonitorProcess monitor) {
            var monitorId = new HashEntry(nameof(monitor.Id), monitor.Id.ToByteArray());
            var processName = new HashEntry(nameof(monitor.ProcessName), monitor.ProcessName);
            var lifeTime = new HashEntry(nameof(monitor.LifeTime), monitor.LifeTime);
            var subscribers = new HashEntry(nameof(monitor.Subscribers), monitor.Subscribers.ToString());

            return new HashEntry[] { monitorId, processName, lifeTime, subscribers };

        }
    }
}
