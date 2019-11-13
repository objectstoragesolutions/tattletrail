using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests {
    [Subject(typeof(Repository))]
    public class when_createmonitor_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            var name = fixture.Create<String>();
            monitor = Mock.Of<MonitorProcess>(x => x.Id == Guid.NewGuid() &&
                        x.MonitorDetails == new MonitorDetails {
                            ProcessName = name,
                            IntervalTime = fixture.Create<int>(),
                            Subscribers = new String[] { }
                        }); 
            database = Mock.Of<IDatabase>(x => x.HashSetAsync( monitor.Id.ToString(), 
                Moq.It.IsAny<HashEntry[]>(),
                CommandFlags.None) == Task.FromResult(default(object)) && 
                x.KeyExpireAsync(monitor.Id.ToString(),
                TimeSpan.FromSeconds(monitor.MonitorDetails.IntervalTime),
                CommandFlags.None)== Task.FromResult(true));
            provider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder()
                .WithRedisServerProvider(provider)
                .Build();
        };
        Because of = async () =>
            await repository.CreateMonitorAsync(monitor);

        It should_call_hashgetallasync_once = () =>
            Mock.Get(provider).Verify(x => x.Database.HashSetAsync(
                monitor.Id.ToString(),
                Moq.It.IsAny<HashEntry[]>(),
                CommandFlags.None), Times.Once);

        It should_set_expire_key_once = () =>
            Mock.Get(provider).Verify(x => x.Database.KeyExpireAsync(
                monitor.Id.ToString(), 
                TimeSpan.FromSeconds(monitor.MonitorDetails.IntervalTime), 
                CommandFlags.None), Times.Once);

        static IRedisServerProvider provider;
        static IDatabase database;
        static MonitorProcess monitor; 
        static Repository repository;
    }
}
