using Machine.Specifications;
using Moq;
using StackExchange.Redis;
using System.Threading.Tasks;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using It = Machine.Specifications.It;

namespace TattleTrail.Tests.DALTests.RepositoryTests {
    [Subject(typeof(Repository))]
    public class when_get_all_monitors_called {
        Establish _context = () => {
            database = Mock.Of<IDatabase>(x => x.HashGetAllAsync("*", CommandFlags.None) ==
                        Task.Run(() => new HashEntry[] { }));
            provider = Mock.Of<IRedisServerProvider>(x => x.Database == database);
            repository = new Builder()
            .WithRedisServerProvider(provider)
            .Build();
        };
        Because of = async () =>
            await repository.GetAllMonitors();

        It should_call_hashgetallasync_once = () => 
            Mock.Get(provider).Verify(x => x.Database.HashGetAllAsync("*", CommandFlags.None), Times.Once);

        static IRedisServerProvider provider;
        static IDatabase database;
        static Repository repository;
    }
}
