using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorModelFactory : IBaseModelFactory<MonitorProcess> {
        public MonitorProcess Create(Guid id, String name, String lifeTime) {
            return new MonitorProcess() { Id = id, ProcessName = name , LifeTime = lifeTime};
        }
    }
}
