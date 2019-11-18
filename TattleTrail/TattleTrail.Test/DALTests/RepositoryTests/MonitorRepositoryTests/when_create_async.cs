using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    [Ignore("Rework")]
    public class when_create_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitor = Mock.Of<MonitorProcess>();
            serverProvider = Mock.Of<IRedisServerProvider>();
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.CreateAsync(monitor);

        It should_add_monitor_to_redis = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry(), CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static MonitorProcess monitor;
        static IRedisServerProvider serverProvider;
    }
}
