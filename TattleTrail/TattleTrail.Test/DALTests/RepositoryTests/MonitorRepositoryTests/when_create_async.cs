using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests.MonitorRepositoryTests {
    [Subject(typeof(MonitorRepository))]
    [Ignore("Looks like i should add multiplexer for mocking")]
    public class when_create_async {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            monitor = Mock.Of<MonitorProcess>(x => x.Id == Guid.NewGuid());
            database = Mock.Of<IDatabase>(x => x.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry(), CommandFlags.None) == Task.CompletedTask);
            serverProvider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder().WithRedisServerProvider(serverProvider).Build();
        };

        Because of = async () =>
                await repository.CreateAsync(monitor);

        It should_add_monitor_to_redis = () =>
                Mock.Get(serverProvider).Verify(x => x.Database.HashSetAsync(monitor.Id.ToString(), monitor.ConvertMonitorToHashEntry(), CommandFlags.None), Times.Once);

        static IMonitorRepository<MonitorProcess> repository;
        static IDatabase database;
        static MonitorProcess monitor;
        static IRedisServerProvider serverProvider;
    }
}
