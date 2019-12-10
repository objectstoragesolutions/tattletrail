using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using System.Net;
using TattleTrail.DAL.RedisServerProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Test.DALTests.RedisServerProviderTests {
    [Subject(typeof(RedisServerProvider))]
    public class when_constructor_called {
        Establish _context = () => {
            dataBase = Mock.Of<IDatabase>();
            server = Mock.Of<IServer>();
            multiplexer = Mock.Of<IConnectionMultiplexer>(x => 
                              x.GetDatabase(-1, null) == dataBase &&
                              x.GetServer(Moq.It.IsAny<String>(), null) == server && 
                              x.GetEndPoints(false) == new EndPoint[] { });
            provider = new Builder()
                         .WithConnectionMultiplexer(multiplexer)
                         .Build();
        };

        It should_call_get_server_once = () => 
            Mock.Get(multiplexer).Verify(x => x.GetServer(Moq.It.IsAny<String>(), null), Times.Once);

        It should_call_get_database_once = () => 
            Mock.Get(multiplexer).Verify(x => x.GetDatabase(-1, null), Times.Once);

        It should_set_server_as_property = () => 
            provider.Server.ShouldEqual(server);

        It should_set_database_as_property = () => 
            provider.Database.ShouldEqual(dataBase);

        static IRedisServerProvider provider;
        static IServer server;
        static IDatabase dataBase;
        static IConnectionMultiplexer multiplexer;
    }
}
