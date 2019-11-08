using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests {
    public class when_getuserasync_called {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            userId = fixture.Create<String>();
            database = Mock.Of<IDatabase>();
            provider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder()
                     .WithRedisServerProvider(provider)
                     .Build();
        };
        Because of = async () =>
            await repository.GetUserAsync(userId);

        It should_call_hashgetallasync_once = () =>
            Mock.Get(provider).Verify(x => x.Database.HashGetAsync(userId, "*", CommandFlags.None), Times.Once);

        static IRedisServerProvider provider;
        static IDatabase database;
        static Repository repository;
        static String userId;
    }
}
