using System;

namespace TattleTrail.Models {
    public class SmtpDataModel {
        public String EmailName { get; set; }
        public String EmailAddress { get; set; }
        public String Client { get; set; }
        public Int32 Port { get; set; }
        public String Login { get; set; }
        public String Password { get; set; }
    }
}
