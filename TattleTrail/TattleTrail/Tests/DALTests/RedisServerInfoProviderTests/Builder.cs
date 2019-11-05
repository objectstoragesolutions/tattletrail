using StackExchange.Redis;
using TattleTrail.DAL.RedisServerInfoProvider;

namespace TattleTrail.Tests.DALTests.RedisServerInfoProviderTests {
    public class Builder {
        IConnectionMultiplexer _multiplexer;
        public RedisServerProvider WithConnectionMultiplexer(IConnectionMultiplexer multiplexer) {
            return new RedisServerProvider(multiplexer);
        }

        public RedisServerProvider Build() {
            return new RedisServerProvider(_multiplexer);
        }
    }
}
