﻿using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorDetailsFactory : IMonitorDetailsFactory {
        public MonitorDetails Create(MonitorDetails details) {
            return new MonitorDetails { ProcessName = details.ProcessName, IntervalTime = details.IntervalTime, Subscribers = details.Subscribers};
        }
    }
}
