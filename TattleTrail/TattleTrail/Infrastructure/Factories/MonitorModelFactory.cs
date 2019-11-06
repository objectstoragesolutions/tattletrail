using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Factories {
    public class MonitorModelFactory : IBaseModelFactory<MonitorModel> {
        public MonitorModel Create(String id, String name, TimeSpan lifeTime) {
            return new MonitorModel() { Id = id, MonitorName = name , LifeTime = lifeTime.ToString()};
        }
    }
}
