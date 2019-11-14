using System;
using System.Threading.Tasks;

namespace TattleTrail.DAL.Repository {
    public interface ICheckInRepository<T> : IRepository<T> where T : class {
        Task CreateAsync(Guid key, Int32 interval);
    }
}
