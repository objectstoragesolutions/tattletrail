using StackExchange.Redis;
using TattleTrail.Models;

namespace TattleTrail.Infrastructure.Extensions {
    public static class UserModelExtensions {
        public static HashEntry[] ConvertUserToHashEntry(this User user) {
            var userId = new HashEntry(nameof(user.Id), user.Id.ToString());
            var userName = new HashEntry(nameof(user.UserName), user.UserName);
            var monitorIds = new HashEntry(nameof(user.MonitorProcessId), user.MonitorProcessId.ToString());

                return new HashEntry[] { userId, userName, monitorIds };

        }
    }
}
