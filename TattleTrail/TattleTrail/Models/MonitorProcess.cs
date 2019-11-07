using System;
using System.Collections.Generic;

namespace TattleTrail.Models {
    public class MonitorProcess {
        public Guid Id { get; set; }
        public String ProcessName { get; set; }
        public Int32 LifeTime { get; set; }
        public HashSet<String> Subscribers { get; set; }
    }
}
