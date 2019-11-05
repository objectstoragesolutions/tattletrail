using System;

namespace TattleTrail.Models {
    public class MonitorModel {
        public MonitorModel(String id, String monitorName) {
            Id = id;
            MonitorName = monitorName;
        }
        public String Id { get; set; }
        public String MonitorName { get; set; }

        public TimeSpan LifeTime { get; set; } = TimeSpan.FromSeconds(1);
    }
}
