using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerInfoProvider;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RedisServerInfoProviderTests {
    [Subject(typeof(RedisServerProvider))]
    public class when_getting_database {
        Establish _context = () => {
            expectedResult = Mock.Of<IDatabase>();
            multiplexer = Mock.Of<IConnectionMultiplexer>(x => x.GetDatabase(-1, null) == expectedResult);
            serverProvider = new Builder().WithConnectionMultiplexer(multiplexer).Build();
        };
        Because of = () => {
            result = serverProvider.GetDatabase();
        };

        It should_call_get_database_once = () => {
            Mock.Get(multiplexer).Verify(x => x.GetDatabase(-1, null), Times.Once);
        };

        It should_return_expected_database = () =>
            result.Equals(expectedResult);

        static IConnectionMultiplexer multiplexer;
        static RedisServerProvider serverProvider;
        static IDatabase expectedResult;
        static IDatabase result;
    }
}
