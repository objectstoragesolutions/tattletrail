using System;
using System.Collections.Generic;

namespace TattleTrail.Models {
    public class User {

        public Guid Id { get; set; }
        public String UserName { get; set; }
        public HashSet<Guid> MonitorIds { get; set; }
    }
}
