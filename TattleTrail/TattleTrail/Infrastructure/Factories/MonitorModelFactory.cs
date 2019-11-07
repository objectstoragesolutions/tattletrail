using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorModelFactory : IBaseModelFactory<Monitor> {
        public Monitor Create(Guid id, String name, TimeSpan lifeTime) {
            return new Monitor() { Id = id, ProcessName = name , LifeTime = lifeTime};
        }
    }
}
