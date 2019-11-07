using System;
using System.Collections.Generic;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorModelFactory : IMonitorModelFactory {
        public MonitorProcess Create(String name, Int32 intervalTime, HashSet<String> subscribers) {
            return new MonitorProcess() { 
                Id = Guid.NewGuid(), 
                ProcessName = name,
                LifeTime = intervalTime,
                Subscribers = subscribers
            };
        }
    }
}
