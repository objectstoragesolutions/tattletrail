using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests {
    [Subject(typeof(Repository))]
    public class when_get_all_monitors_called {
        Establish _context = () => {
            Fixture fixture = new Fixture(); 
            guidKey = fixture.Create<Guid>();
            database = Mock.Of<IDatabase>(x => x.HashGetAllAsync("*", CommandFlags.None) ==
                        Task.FromResult(new HashEntry[] { }));
            server = Mock.Of<IServer>(x => x.Keys(0, "*", 10, 0, 0, CommandFlags.None) == new HashSet<RedisKey> { $"{guidKey}"});
            provider = Mock.Of<IRedisServerProvider>(x => x.Database == database && x.Server == server);
            repository = new Builder()
            .WithRedisServerProvider(provider)
            .Build();
        };
        Because of = async () =>
            result = await repository.GetAllMonitors();

        It should_call_dataprovider_server_keys_once = () => 
            Mock.Get(provider).Verify(x => x.Server.Keys( 0,"*", 10, 0, 0, CommandFlags.None), Times.Once);

        It should_call_get_monitor_async_once = () => 
            Mock.Get(provider).Verify(x => x.Database.HashGetAllAsync(guidKey.ToString(), CommandFlags.None), Times.Once);

        It should_contain_proper_result = () =>
            result.Count.Equals(1);

        static IRedisServerProvider provider;
        static IDatabase database;
        static IServer server;
        static Guid guidKey;
        static HashSet<MonitorProcess> result;
        static Repository repository;
    }
}
