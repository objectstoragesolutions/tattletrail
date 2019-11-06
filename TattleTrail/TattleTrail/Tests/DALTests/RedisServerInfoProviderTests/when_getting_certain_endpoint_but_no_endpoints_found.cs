using AutoFixture;
using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using TattleTrail.DAL.RedisServerInfoProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RedisServerInfoProviderTests {
    [Subject(typeof(RedisServerProvider))]
    public class when_getting_certain_endpoint_but_no_endpoints_found {
        Establish _context = () => {
            Fixture fixture = new Fixture();
            id = fixture.Create<int>();
            multiplexer = Mock.Of<IConnectionMultiplexer>();
            serverProvider = new Builder().WithConnectionMultiplexer(multiplexer).Build();
        };
        Because of = () => {
            result = serverProvider.GetCertainEndpoint(id);
        };

        It should_return_empty_string = () => {
            result.Equals(String.Empty);
        };

        static IConnectionMultiplexer multiplexer;
        static RedisServerProvider serverProvider;
        static int id;
        static String result;
    }
}
