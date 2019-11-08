using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Models;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests {
    public class when_createuserasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            userName = fixture.Create<String>();
            user = Mock.Of<User>(x => x.Id == Guid.NewGuid() && 
                    x.MonitorProcessId == fixture.Create<HashSet<Guid>>() && 
                    x.UserName == userName);
            database = Mock.Of<IDatabase>();
            provider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder()
            .WithRedisServerProvider(provider)
            .Build();
        };
        Because of = async () =>
            await repository.CreateUserAsync(user);

        It should_call_hashgetallasync_once = () =>
            Mock.Get(provider).Verify(x => x.Database.HashSetAsync(
                Moq.It.IsAny<RedisKey>(),
                Moq.It.IsAny<HashEntry[]>(), 
                CommandFlags.None), Times.Once);

        static IRedisServerProvider provider;
        static IDatabase database;
        static Repository repository;
        static User user;
        static String userName;
    }
}
