using Moq;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerProvider;

namespace TattleTrail.Tests.DALTests.RedisServerProviderTests {
    public class Builder {
        private IConnectionMultiplexer _multiplexer = Mock.Of<IConnectionMultiplexer>();

        public Builder WithConnectionMultiplexer(IConnectionMultiplexer multiplexer) {
            _multiplexer = multiplexer;
            return this;
        }

        public RedisServerProvider Build() {
            return new RedisServerProvider(_multiplexer);
        }
    }
}
