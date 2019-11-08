using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests {
    [Subject(typeof(Repository))]
    public class when_add_monitor_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            var name = fixture.Create<String>();
            monitor = Mock.Of<MonitorProcess>(x => x.Id == Guid.NewGuid() && 
                        x.ProcessName == name && 
                        x.LifeTime == fixture.Create<int>() &&
                        x.Subscribers == new String[] { });
            database = Mock.Of<IDatabase>();
            provider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder()
            .WithRedisServerProvider(provider)
            .Build();
        };
        Because of = async () =>
            await repository.AddMonitorAsync(monitor);

        It should_call_hashgetallasync_once = () =>
            Mock.Get(provider).Verify(x => x.Database.HashSetAsync(
                monitor.Id.ToString(),
                Moq.It.IsAny<HashEntry[]>(),
                CommandFlags.None), Times.Once);

        static IRedisServerProvider provider;
        static IDatabase database;
        static MonitorProcess monitor; 
        static Repository repository;
    }
}
