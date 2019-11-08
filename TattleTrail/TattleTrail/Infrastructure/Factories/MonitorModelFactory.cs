using System;
using System.Collections.Generic;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorModelFactory : IMonitorModelFactory {
        public MonitorProcess Create(String processName, Int32 intervalTime, HashSet<String> subscribers) {
            return new MonitorProcess() { 
                Id = Guid.NewGuid(), 
                ProcessName = processName,
                LifeTime = intervalTime,
                Subscribers = subscribers
            };
        }
    }
}
