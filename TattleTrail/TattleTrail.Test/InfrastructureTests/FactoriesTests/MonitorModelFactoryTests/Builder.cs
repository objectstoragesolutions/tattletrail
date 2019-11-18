using Moq;
using TattleTrail.Infrastructure.Factories;

namespace TattleTrail.Tests.InfrastructureTests.FactoriesTests.MonitorModelFactoryTests {
    public class Builder {

        private IMonitorDetailsFactory _factory = Mock.Of<IMonitorDetailsFactory>();

        public Builder WithMonitorDetailsFactory(IMonitorDetailsFactory factory) {
            _factory = factory;
            return this;
        }

        public MonitorModelFactory Build() {
            return new MonitorModelFactory(_factory);
        }
    }
}
