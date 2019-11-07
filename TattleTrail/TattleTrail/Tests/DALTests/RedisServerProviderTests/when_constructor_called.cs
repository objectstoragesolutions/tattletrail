using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Net;
using TattleTrail.DAL.RedisServerProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RedisServerProviderTests {
    //TODO: Rework test
    [Subject(typeof(RedisServerProvider))]
    [Ignore("Rework Later")]
    public class when_constructor_called {
        Establish _context = () => {
            multiplexer = Mock.Of<IConnectionMultiplexer>(x => x.GetEndPoints(false) == new EndPoint[] { });
            provider = new Builder()
            .WithConnectionMultiplexer(multiplexer)
            .Build();

        };

        It should_call_get_server_once = () => {
            Mock.Get(multiplexer).Verify(x => x.GetServer(Moq.It.IsAny<String>(), null), Times.Once);
        };

        It should_call_get_database_once = () => {
            Mock.Get(multiplexer).Verify(x => x.GetDatabase(-1, null), Times.Once);
        };

        It should_set_server_as_property = () => {
            provider.Server.Equals(multiplexer.GetServer(Moq.It.IsAny<String>(), null));
        };

        It should_set_database_as_property = () => {
            provider.Database.Equals(multiplexer.GetDatabase(-1, null));
        };

        static IRedisServerProvider provider;
        static IConnectionMultiplexer multiplexer;
    }
}
