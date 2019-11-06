using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using TattleTrail.DAL.RedisServerInfoProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RedisServerInfoProviderTests {
    [Subject(typeof(RedisServerProvider))]
    public class when_getting_server {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            host = fixture.Create<String>();
            expectedResult = Mock.Of<IServer>();
            multiplexer = Mock.Of<IConnectionMultiplexer>(x => x.GetServer(host, null) == expectedResult);
            serverProvider = new Builder().WithConnectionMultiplexer(multiplexer).Build();
        };
        Because of = () => {
            result = serverProvider.GetServer(host);
        };

        It should_call_get_server_once = () => {
            Mock.Get(multiplexer).Verify(x => x.GetServer(host, null), Times.Once);
        };

        It should_return_expected_server = () =>
            result.Equals(expectedResult);

        static IConnectionMultiplexer multiplexer;
        static String host;
        static RedisServerProvider serverProvider;
        static IServer expectedResult;
        static IServer result;
    }
}
