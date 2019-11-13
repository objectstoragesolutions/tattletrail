using Moq;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Factories;

namespace TattleTrail.Tests.DALTests.RepositoryTests {
    public class Builder {
        IRedisServerProvider _provider = Mock.Of<IRedisServerProvider>();
        ICheckInModelFactory _checkInModel = Mock.Of<ICheckInModelFactory>();
        public Builder WithRedisServerProvider(IRedisServerProvider provider) {
            _provider = provider;
            return this;
        }

        public Repository Build() {
            return new Repository(_provider, _checkInModel);
        }
    }
}
