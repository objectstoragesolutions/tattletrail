using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System;
using TattleTrail.DAL.RedisServerInfoProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RedisServerInfoProviderTests {
    [Subject(typeof(RedisServerProvider))]
    [Ignore("Finish test cases")]
    public class when_getting_certain_endpoint {
        Establish _context = () => {
            //expectedResult = Mock.Of<EndPoint[]>();
            multiplexer = Mock.Of<IConnectionMultiplexer>(/*x => x.GetEndPoints(false) == expectedResult*/);
            serverProvider = new Builder().WithConnectionMultiplexer(multiplexer).Build();
        };
        Because of = () => {
            result = serverProvider.GetCertainEndpoint(0);
        };

        It should_call_get_end_points_once = () => {
            Mock.Get(multiplexer).Verify(x => x.GetEndPoints(false), Times.Once);
        };

        It should_return_expected_server = () => { };
           // result.Equals(expectedResult);

        static IConnectionMultiplexer multiplexer;
        static RedisServerProvider serverProvider;
        //static EndPoint[] expectedResult;
        static String result;
    }
}
