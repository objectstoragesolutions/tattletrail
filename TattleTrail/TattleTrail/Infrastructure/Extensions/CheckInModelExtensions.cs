using System;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class CheckInModelExtensions {
        public static String CreateKeyString(this CheckIn checkIn) {
            return nameof(CheckIn.CheckInId).ToLower() + ":" + checkIn.CheckInId.ToString();
        }
    }
}
