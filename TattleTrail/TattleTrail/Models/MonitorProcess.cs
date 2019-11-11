using System;
using System.Collections.Generic;

namespace TattleTrail.Models {
    public class MonitorProcess {
        public Guid Id { get; set; }

        public MonitorDetails MonitorDetails { get; set; } = new MonitorDetails();
    }
}
