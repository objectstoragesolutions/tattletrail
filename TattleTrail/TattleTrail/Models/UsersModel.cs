using System;
using System.Collections.Generic;

namespace TattleTrail.Models {
    public class UsersModel {
        public String UserName { get; set; }
        public HashSet<String> MonitorIds { get; set; }
    }
}
