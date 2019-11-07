using System;
using System.Collections.Generic;

namespace TattleTrail.Models {
    public class Monitor {
        public Guid Id { get; set; }
        public String ProcessName { get; set; }
        public HashSet<String> Subscribers { get; set; }
        public TimeSpan LifeTime { get; set ; }
    }
}
