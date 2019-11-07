using System;
using System.Collections.Generic;

namespace TattleTrail.Models {
    public class MonitorProcess {
        public Guid Id { get; set; }
        public String ProcessName { get; set; }
        //TODO: Fix problem with serialisation
        //public TimeSpan LifeTime { get; set; }
        public String LifeTime { get; set; }
        public HashSet<String> Subscribers { get; set; }
    }
}
