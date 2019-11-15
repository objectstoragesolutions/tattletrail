using System;

namespace TattleTrail.Models {
    public class MonitorDetails {
        public String ProcessName { get; set; }
        public Int32 IntervalTime { get; set; }
        public String[] Subscribers { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
        public DateTime LastCheckIn { get; set; }
        public Boolean IsDown { get; set; }
    }
}
