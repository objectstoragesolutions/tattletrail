using System;

namespace TattleTrail.Infrastructure.Extensions {
    public static class GuidExtensions {

        public static byte[] NewRedisKey(this Guid guid) {
            return Guid.NewGuid().ToByteArray();
        }
    }
}
