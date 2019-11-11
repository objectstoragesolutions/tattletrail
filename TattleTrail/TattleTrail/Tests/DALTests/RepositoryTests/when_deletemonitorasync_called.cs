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
    public class when_deletemonitorasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            var name = fixture.Create<String>();
            monitor = Mock.Of<MonitorProcess>(x => x.Id == Guid.NewGuid());
            database = Mock.Of<IDatabase>();
            provider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder()
            .WithRedisServerProvider(provider)
            .Build();
        };
        Because of = async () =>
            await repository.DeleteMonitorAsync(monitor.Id);

        It should_call_hashgetallasync_once = () =>
            Mock.Get(provider).Verify(x => x.Database.KeyDeleteAsync(monitor.Id.ToString(), CommandFlags.None), Times.Once);

        static IRedisServerProvider provider;
        static IDatabase database;
        static MonitorProcess monitor;
        static Repository repository;
    }
}
