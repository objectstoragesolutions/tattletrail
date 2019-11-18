using System;

namespace TattleTrail.Models {
    public class MonitorProcess {
        public Guid Id { get; set; }
        public MonitorDetails MonitorDetails { get; set; } = new MonitorDetails();
    }
}
