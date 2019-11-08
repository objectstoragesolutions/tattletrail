using System;
using System.Collections.Generic;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorModelFactory : IMonitorModelFactory {
        public MonitorProcess Create(String processName, Int32 intervalTime, String[] subscribers) {
            return new MonitorProcess() { 
                Id = Guid.NewGuid(), 
                ProcessName = processName,
                LifeTime = intervalTime,
                Subscribers = subscribers
            };
        }

        public MonitorProcess Create(Guid id, String processName, Int32 intervalTime, String[] subscribers) {
            return new MonitorProcess() {
                Id = id,
                ProcessName = processName,
                LifeTime = intervalTime,
                Subscribers = subscribers
            };
        }
    }
}
