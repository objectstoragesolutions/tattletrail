using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    public class when_delete_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitor = Mock.Of<MonitorProcess>();
            database = Mock.Of<IDatabase>(x => x.KeyDeleteAsync(monitor.Id.ToString(), CommandFlags.None) == Task.FromResult(true));
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.DeleteAsync(monitor.Id);

        It should_add_monitor_to_redis = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.KeyDeleteAsync(monitor.Id.ToString(), CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static IDatabase database;
        static MonitorProcess monitor;
        static IRedisServerProvider serverProvider;
    }
}
