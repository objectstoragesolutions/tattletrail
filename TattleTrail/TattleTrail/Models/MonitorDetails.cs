using System;
using System.Collections.Generic;

namespace TattleTrail.Models {
    public class MonitorDetails {
        public String ProcessName { get; set; }
        public Int32 LifeTime { get; set; }
        public String[] Subscribers { get; set; }
    }
}
