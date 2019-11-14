using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TattleTrail.DAL.Repository {
    public interface IRepository<T> where T : class {
        Task<HashSet<T>> GetAllAsync();
        Task<T> GetAsync(RedisKey key);
    }
}
